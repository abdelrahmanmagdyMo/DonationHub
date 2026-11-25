
using DonationHub.Domain.Common.Results.Abstractions;
using DonationHub.Domain.Persistence.Identity;
using DonationHub.Infrastructure.Options;
using DonationHub.Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace DonationHub.Infrastructure.Persistence.Seeding
{
    public static class AdminSeeder
    {
        public static async Task<Result<ApplicationUser>> SeedAdmin(
            SeedAdminOptions seedAdmin,
            UserManager<ApplicationUser> userManager,
            TimeProvider timeProvider)
        {
            var adminEmail = seedAdmin.Email;
            var adminPassword = seedAdmin.Password;
            var fullName = seedAdmin.FullName;


            var admin = await userManager.FindByEmailAsync(adminEmail);


            if (admin is null)
            {
                var adminResult = ApplicationUser.Create(
                       fullName,
                       adminEmail,
                       PrefferedLanguage.EN,
                       timeProvider.GetUtcNow());
                if (adminResult.IsFailure) return Result<ApplicationUser>.Failure(adminResult.Error);

                var result = await userManager.CreateAsync(
                    admin,
                    adminPassword);

                if (!result.Succeeded)
                {
                    var errors = new StringBuilder();
                    foreach (var error in result.Errors)
                    {
                        errors.AppendLine(error.Description);
                    }

                    throw new InvalidOperationException(
                        $"Failed to create admin user: {errors}");
                }
            }


            if (!await userManager.IsInRoleAsync(admin, "Admin"))
            {
                var roleResult = await userManager.AddToRoleAsync(
                    admin,
                    "Admin");

                if (!roleResult.Succeeded)
                {
                    var errors = new StringBuilder();
                    foreach (var error in roleResult.Errors)
                    {
                        errors.AppendLine(error.Description);
                    }

                    throw new InvalidOperationException(
                        $"Failed to assign Admin role: {errors}");
                }
            }
            return Result<ApplicationUser>.Success(admin);
        }

    }
}