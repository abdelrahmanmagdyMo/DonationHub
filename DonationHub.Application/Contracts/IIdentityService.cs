using DonationHub.Application.Authentications.DTOs;
using DonationHub.Application.Authentications.Responses;
using DonationHub.Domain.Common.Results.Abstractions;

namespace DonationHub.Application.Contracts
{
    public interface IIdentityService
    {
        Task<Result<UserDto>> GetUserByIdAsync(Guid UserId, CancellationToken ct);
        Task<Result<UserDto>> GetUserByEmailAsync(string Email, CancellationToken ct);
        Task<Result<UserDto>> FindByIdAsync(Guid UserId, CancellationToken ct);
        Task<Result<IEnumerable<string>>> GetRolesAsync(Guid UserId, CancellationToken ct);
        Task<Result<AdminRegisterResponse>> CreateStaffUserAsync(string fullName, string email, string role, CancellationToken ct);
        Task<bool> CheckPasswordAsync(string email, string password, CancellationToken ct = default);
        Task<Result<string>> CreateUserAsync(RegisterDto register, CancellationToken ct);

    }
}
