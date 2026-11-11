using DonationHub.Application.IdentityExceptions;
using DonationHub.Domain.Persistence.Identity;
using DonationHub.Infrastructure.Options;
using DonationHub.Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace DonationHub.Infrastructure.Persistence.Seeding
{
    public static class DonorSeeder
    {
        public static async Task SeedDonor(SeedDonorOptions options,
             UserManager<ApplicationUser> userManager, DateTimeOffset dateTime)


        {
            var donorEmail = options.Email;
            var donorPassword = options.Password;
            var fullName = options.FullName;
            var user = await userManager.FindByEmailAsync(options.Email);
            if (user is null)
            {
                user = ApplicationUser.Create(fullName, donorEmail, PrefferedLanguage.EN, DateTime.UtcNow);
                var result = await userManager.CreateAsync(user, donorPassword);
                if (!result.Succeeded) throw new RegistrationFailedException(AuthErrors.InvalidCredentials, "Seeding Failed");
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

                    throw new InvalidOperationException(
                        $"Failed to assign Donor role: {errors}");
                }
            }
        }
    }
}

