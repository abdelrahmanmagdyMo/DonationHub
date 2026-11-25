using DonationHub.Application.Authentications.DTOs;
using DonationHub.Application.Authentications.Helpers;
using DonationHub.Application.Authentications.Responses;
using DonationHub.Application.Contracts;
using DonationHub.Domain.Common.Results.Abstractions;
using DonationHub.Domain.Persistence.Identity;
using DonationHub.Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using AuthErrors = DonationHub.Application.IdentityExceptions;
namespace DonationHub.Infrastructure.Services
{
    public class IdentityService(UserManager<ApplicationUser> userManager, TimeProvider timeProvider)
        : IIdentityService
    {
        private static readonly string[] AllowedStaffRoles = ["Admin", "CampaignManager"];
        public async Task<Result<UserDto>> GetUserByIdAsync(Guid UserId, CancellationToken ct)
        {
            var user = await userManager.FindByIdAsync(UserId.ToString());
            if (user is null) return Result<UserDto>.Failure(AuthErrors.AuthErrors.InvalidCredentials);
            var roles = await userManager.GetRolesAsync(user);
            var userDto = new UserDto(UserId, user.Email!, user.FullName, user.IsActive, roles);
            return Result<UserDto>.Success(userDto);
        }
        public async Task<Result<UserDto>> GetUserByEmailAsync(string email, CancellationToken ct)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user is null) return Result<UserDto>.Failure(AuthErrors.AuthErrors.InvalidCredentials);
            var roles = await userManager.GetRolesAsync(user);
            var userDto = new UserDto(user.Id, user.Email!, user.FullName, user.IsActive, roles);
            return Result<UserDto>.Success(userDto);
        }
        public Task<Result<UserDto>> FindByIdAsync(Guid UserId, CancellationToken ct)
            => GetUserByIdAsync(UserId, ct);

        public async Task<Result<AdminRegisterResponse>> CreateStaffUserAsync(string fullName, string email, string role, CancellationToken ct)
        {
            if (!AllowedStaffRoles.Contains(role))
                return Result<AdminRegisterResponse>.Failure(AuthErrors.AuthErrors.InvalidRole);
            if (await userManager.FindByEmailAsync(email) is not null)
                return Result<AdminRegisterResponse>.Failure(AuthErrors.AuthErrors.EmailAlreadyExists);

            var userResult = ApplicationUser.Create(fullName, email, PrefferedLanguage.EN, timeProvider.GetUtcNow().UtcDateTime);
            if (userResult.IsFailure) return Result<AdminRegisterResponse>.Failure(userResult.Error);

            var tempPassword = GenerateTemporaryPassword.GeneratePassword(12);
            var createResult = await userManager.CreateAsync(userResult.Value, tempPassword);
            if (!createResult.Succeeded) return Result<AdminRegisterResponse>.Failure(AuthErrors.AuthErrors.RegistrationFailed);
            var roleResult = await userManager.AddToRoleAsync(userResult.Value, role);
            if (!roleResult.Succeeded)
            {
                await userManager.DeleteAsync(userResult.Value);
                return Result<AdminRegisterResponse>.Failure(AuthErrors.AuthErrors.RegistrationFailed);
            }
            return Result<AdminRegisterResponse>.Success(new AdminRegisterResponse
            {
                Email = email,
                Role = role,
                TemporaryPassword = tempPassword
            });
        }
        public async Task<Result<IEnumerable<string>>> GetRolesAsync(Guid UserId, CancellationToken ct)
        {
            var user = await userManager.FindByIdAsync(UserId.ToString());
            if (user is null) return Result<IEnumerable<string>>.Failure(AuthErrors.AuthErrors.InvalidCredentials);
            var roles = await userManager.GetRolesAsync(user);
            return Result<IEnumerable<string>>.Success(roles.ToList());
        }

        public async Task<bool> CheckPasswordAsync(string email, string password, CancellationToken ct = default)
        {
            var user = await userManager.FindByEmailAsync(email);
            return user is not null && await userManager.CheckPasswordAsync(user, password);
        }
        public async Task<Result<string>> CreateUserAsync(RegisterDto register, CancellationToken ct)
        {
            var userResult = ApplicationUser.Create(register.FullName, register.Email, PrefferedLanguage.EN, timeProvider.GetUtcNow());
            if (userResult.IsFailure) return Result<string>.Failure(userResult.Error);
            var createUserResult = await userManager.CreateAsync(userResult.Value, register.Password);
            if (!createUserResult.Succeeded)
            {
                var errors = "";
                foreach (var error in createUserResult.Errors)
                {
                    errors = string.Join(", ", createUserResult.Errors.Select(c => c.Description));
                }

                return Result<string>.Failure(new Domain.Common.Results.Error("Auth.InvalidCredentials", errors));
            }
            return Result<string>.Success("new user created Successfully");
        }

    }
}