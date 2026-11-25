using DonationHub.Application.Authentications.DTOs;
using DonationHub.Application.Contracts;
using DonationHub.Domain.Identity.RefreshToken;
using DonationHub.Infrastructure.Options;
using DonationHub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DonationHub.Infrastructure.Services
{
    public class
       TokenService(
                    TimeProvider _time,
                    IOptions<JwtOptions> _jwt,
                    AppDbContext context) :
        ITokenService
    {
        public string GenerateAccessToken(TokenDtoRequest tokenDto)
        {
            var claims = new List<Claim>()
           {
               new Claim(JwtRegisteredClaimNames.Sub,tokenDto.UserId.ToString()),
               new Claim(ClaimTypes.NameIdentifier,tokenDto.UserId.ToString()),
               new Claim(JwtRegisteredClaimNames.Email,tokenDto.Email!),
               new Claim(JwtRegisteredClaimNames.Name,tokenDto.FullName)
           };
            claims.AddRange(tokenDto.Roles.Select(role => new Claim(ClaimTypes.Role, role)));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Value.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiresOn = _time.GetUtcNow().UtcDateTime.AddMinutes(_jwt.Value.ExpiryMinutes);
            var jwtIssuer = _jwt.Value.Issuer;
            var jwtAudience = _jwt.Value.Audience;
            var now = _time.GetUtcNow().UtcDateTime;
            var token = new JwtSecurityToken
                (jwtIssuer, jwtAudience, claims, now, expiresOn, credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public string GenerateRefreshToken()
        {
            var bytes = RandomNumberGenerator.GetBytes(64);
            return Convert.ToBase64String(bytes);
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

        public async Task RevokeRefreshTokenAsync(RefreshTokenRequestDto dto, CancellationToken ct = default)
        {
            var principal = GetPrincipalFromExpiredToken(dto.token);
            if (principal is null) return;
            if (!Guid.TryParse(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var UserID)) throw new Exception();
            var now = _time.GetUtcNow();
            var hashToken = Hash(dto.RefreshToken);
            var existingToken = context
           .RefreshTokens.FirstOrDefault(x => x.TokenHash == hashToken &&
            x.UserId == UserID && x.RevokedAt == null &&
           x.ExpiresAt > now);
            if (existingToken is null) throw new Exception();
            existingToken.Revoke(_time.GetUtcNow());
            await context.SaveChangesAsync(ct);
        }
        public string Hash(string token)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(token));
            return Convert.ToBase64String(bytes);
        }

        public async Task<RefreshToken?> GetHashAsync(string hashToken, CancellationToken ct)
        => await context.RefreshTokens.FirstOrDefaultAsync(c => c.TokenHash == hashToken, ct);



        public void AddRefreshToken(RefreshToken refreshToken)
        => context.RefreshTokens.Add(refreshToken);
        public DateTimeOffset GetRefreshTokenExpiry(DateTimeOffset now) =>
         now.AddDays(_jwt.Value.RefreshTokenExpiryDays);
    }
}
