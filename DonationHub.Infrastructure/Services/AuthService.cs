
using DonationHub.Application.Authentications.DTOs;
using DonationHub.Application.Authentications.Responses;
using DonationHub.Application.Features.Auth;
using DonationHub.Application.IdentityExceptions;
using DonationHub.Domain.Persistence.Identity;
using DonationHub.Infrastructure.IdentityExceptions;
using DonationHub.Infrastructure.Options;
using DonationHub.Infrastructure.Persistence;
using DonationHub.Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DonationHub.Infrastructure.Services
{
    public class AuthService
        (AppDbContext context, IOptions<JwtOptions> _jwt, UserManager<ApplicationUser> userManager)
    : IAuthService
    {
        private string GenerateToken(ApplicationUser user, IEnumerable<string> roles)
        {
            var claims = new List<Claim>()
           {
               new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
               new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
               new Claim(JwtRegisteredClaimNames.Email,user.Email!),
               new Claim(JwtRegisteredClaimNames.Name,user.FullName)
           };
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Value.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiresOn = DateTime.UtcNow.AddMinutes(_jwt.Value.ExpiryMinutes);
            var jwtIssuer = _jwt.Value.Issuer;
            var jwtAudience = _jwt.Value.Audience;
            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken
                (jwtIssuer, jwtAudience, claims, now, expiresOn, credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<LoginResponse> LoginAsync(LoginDto dto)
        {
            var user = await userManager.FindByEmailAsync(dto.Email);
            if (user is null || !await userManager.CheckPasswordAsync(user, dto.Password))
                throw new Exception("Invalid Credentials");
            var roles = await userManager.GetRolesAsync(user);
            var now = DateTimeOffset.UtcNow;
            var accessToken = GenerateToken(user, roles);
            var refreshToken = GenerateRefreshToken();
            var expiresAt = DateTime.UtcNow.AddDays(_jwt.Value.RefreshTokenExpiryDays);
            var refresh = RefreshToken.Create(user.Id, Hash(refreshToken), now, expiresAt);
            context.RefreshTokens.Add(refresh);
            await context.SaveChangesAsync();
            return new LoginResponse(accessToken, refreshToken);
        }


        private static string GenerateRefreshToken()
        {
            var bytes = RandomNumberGenerator.GetBytes(64);
            return Convert.ToBase64String(bytes);
        }


        public async Task<RegisterResponse> RegisterAsync(RegisterDto dto)
        {
            var user = ApplicationUser.Create(dto.FullName, dto.Email, dto.PrefferedLanguage, DateTimeOffset.UtcNow);

            var result = await userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded) throw new Exception("Failed To Create User");
            var roleResult = await userManager.AddToRoleAsync(user, "Donor");
            if (!result.Succeeded) throw new Exception(result.Errors.ToString());
            return new RegisterResponse(dto.Email, "Created Successfully");
        }

        public async Task RevokeRefreshTokenAsync(RefreshTokenRequestDto dto)
        {
            var principal = GetPrincipalFromExpiredToken(dto.token);
            if (principal is null) throw new ForbiddenAppException(AuthErrors.InvalidCredentials, "User Is Invalid");
            if (!Guid.TryParse(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var UserID)) throw new Exception();
            var now = DateTimeOffset.UtcNow;
            var hashToken = Hash(dto.RefreshToken);
            var existingToken = context
           .RefreshTokens.FirstOrDefault(x => x.TokenHash == hashToken &&
            x.UserId == UserID && x.RevokedAt == null &&
           x.ExpiresAt > now);
            if (existingToken is null) throw new Exception();
            existingToken.Revoke();
            await context.SaveChangesAsync();

        }
        public async Task<UserDto?> GetUserByIdAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null) return null;
            var roles = await userManager.GetRolesAsync(user);
            return new UserDto(user.Id.ToString(), user.Email!, user.FullName, roles);
        }
        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwt.Value.Issuer,
                ValidAudience = _jwt.Value.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Value.SecretKey)),
                ClockSkew = TimeSpan.Zero
            };
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(
                    token,
                    tokenValidationParameters,
                    out var securityToken);
                if (securityToken is not JwtSecurityToken jwtSecurityToken
                    || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    {
                        throw new SecurityTokenException("Invalid token");
                    }
                }
                return principal;
            }
            catch
            {
                return null;
            }
        }

        public async Task<RefreshTokenResponse> RefreshTokenAsync(RefreshTokenRequestDto dto)
        {
            var principal = GetPrincipalFromExpiredToken(dto.token)
                ?? throw new ForbiddenAppException(AuthErrors.InvalidCredentials, "User Is Invalid");

            if (!Guid.TryParse(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
                throw new ForbiddenAppException(AuthErrors.InvalidCredentials, "User Is Invalid");

            var now = DateTimeOffset.UtcNow;
            var oldHash = Hash(dto.RefreshToken);

            var oldToken = await context.RefreshTokens
                .FirstOrDefaultAsync(x => x.TokenHash == oldHash
                                       && x.UserId == userId
                                       && x.RevokedAt == null
                                       && x.ExpiresAt > now)
                ?? throw new ForbiddenAppException(AuthErrors.InvalidCredentials, "Invalid refresh token");

            var user = await userManager.FindByIdAsync(userId.ToString())
                ?? throw new ForbiddenAppException(AuthErrors.InvalidCredentials, "User Is Invalid");

            var roles = await userManager.GetRolesAsync(user);
            var newAccessToken = GenerateToken(user, roles);

            var newRefreshToken = GenerateRefreshToken();
            var newEntity = RefreshToken.Create(userId, Hash(newRefreshToken), now,
                now.AddDays(_jwt.Value.RefreshTokenExpiryDays));

            oldToken.Revoke();
            context.RefreshTokens.Add(newEntity);

            await context.SaveChangesAsync(); // revoke + add في transaction واحدة

            return new RefreshTokenResponse(newAccessToken, newRefreshToken);
        }
        private static string Hash(string token)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(token));
            return Convert.ToBase64String(bytes);
        }

        public async Task RegisterAdminORCampignManager(AdminRegisterDto dto)
        {
            if (dto is null) throw new Exception();
            var existingUserName = await userManager.FindByNameAsync(dto.FullName);
            if (existingUserName is not null) throw new Exception();
            var existingEmail = await userManager.FindByEmailAsync(dto.Email);
            if (existingEmail is not null) throw new Exception();
            var admin = ApplicationUser.Create(dto.FullName, dto.Email, PrefferedLanguage.EN, DateTime.UtcNow);
            var result = await userManager.CreateAsync(admin, dto.Password);
            if (!result.Succeeded) throw new Exception();
            var roleResult = await userManager.AddToRoleAsync(admin, "Admin");
            if (!roleResult.Succeeded) throw new Exception();

        }
    }

}
