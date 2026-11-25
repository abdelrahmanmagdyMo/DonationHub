using DonationHub.Domain.Common.Results;
using DonationHub.Domain.Common.Results.Abstractions;
using DonationHub.Domain.Persistence.Identity;
using DonationHub.Infrastructure.Options;
using DonationHub.Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace DonationHub.Infrastructure.Persistence.Seeding
{
    public static class DonorSeeder
    {
        public static async Task<Result<ApplicationUser>> SeedDonor(SeedDonorOptions options,
             UserManager<ApplicationUser> userManager, DateTimeOffset dateTime)


        {
            var donorEmail = options.Email;
            var donorPassword = options.Password;
            var fullName = options.FullName;
            var user = await userManager.FindByEmailAsync(options.Email);
            if (user is null)
            {
                var userResult = ApplicationUser.Create(fullName, donorEmail, PrefferedLanguage.EN, DateTime.UtcNow);
                if (userResult.IsFailure) return Result<ApplicationUser>.Failure(userResult.Error);
                var result = await userManager.CreateAsync(user, donorPassword);
                if (!result.Succeeded)
                {
                    var errors = new StringBuilder();
                    foreach (var error in result.Errors)
                    {
                        errors.AppendLine(error.Description);
                    }
                    return Result<ApplicationUser>.Failure(new Error("", ""));
                }
            }
            if (!await userManager.IsInRoleAsync(user, "Donor"))
            {
                var roleResult = await userManager.AddToRoleAsync(user, "Donor");
                if (!roleResult.Succeeded)
                {
                    var errors = new StringBuilder();
                    foreach (var error in roleResult.Errors)
                    {
                        errors.AppendLine(error.Description);
                    }
                    return Result<ApplicationUser>.Failure(new Error("", ""));
                }
            }
            return Result<ApplicationUser>.Success(user);
        }
    }
}

