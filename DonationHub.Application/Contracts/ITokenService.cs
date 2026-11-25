using DonationHub.Application.Authentications.DTOs;
using DonationHub.Domain.Identity.RefreshToken;
using System.Security.Claims;

namespace DonationHub.Application.Contracts
{
    public interface ITokenService
    {
        string GenerateAccessToken(TokenDtoRequest tokenDto);
        string GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
        DateTimeOffset GetRefreshTokenExpiry(DateTimeOffset now);
        Task RevokeRefreshTokenAsync(RefreshTokenRequestDto dto, CancellationToken ct);
        string Hash(string token);
        Task<RefreshToken?> GetHashAsync(string hashToken, CancellationToken ct);
        void AddRefreshToken(RefreshToken refreshToken);
    }
}
