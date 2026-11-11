
using DonationHub.Application.Authentications.DTOs;
using DonationHub.Application.Authentications.Responses;
using System.Security.Claims;

namespace DonationHub.Application.Features.Auth
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginDto dto);
        Task<RefreshTokenResponse> RefreshTokenAsync(RefreshTokenRequestDto refreshTokenRequesDto);
        Task<RegisterResponse> RegisterAsync(RegisterDto dto);
        Task RegisterAdminORCampignManager(AdminRegisterDto dto);


        Task RevokeRefreshTokenAsync(RefreshTokenRequestDto dto);
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
        Task<UserDto?> GetUserByIdAsync(string userId);
    }
}
