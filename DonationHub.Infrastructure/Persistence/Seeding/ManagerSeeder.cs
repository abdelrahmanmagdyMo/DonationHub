using DonationHub.Domain.Persistence.Identity;
using DonationHub.Infrastructure.Options;
using DonationHub.Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace DonationHub.Infrastructure.Persistence.Seeding
{
    public static class ManagerSeeder
    {
        public static async Task SeedManager(CampaignManagerSeedOptions options,
             UserManager<ApplicationUser> userManager, DateTimeOffset dateTime)
        {
            var ManagerEmail = options.Email;
            var ManagerPassword = options.Password;
            var fullName = options.FullName;
            var user = await userManager.FindByEmailAsync(options.Email);
            if (user is null)
            {
                user = ApplicationUser.Create(fullName, ManagerEmail, PrefferedLanguage.EN, DateTimeOffset.UtcNow);
                var result = await userManager.CreateAsync(user, ManagerPassword);
                if (!result.Succeeded)
                {
                    var errors = new StringBuilder();
                    foreach (var error in result.Errors)
                    {
                        errors.AppendLine(error.Description);
                    }

                    throw new InvalidOperationException(
                        $"Failed to assign CampaignManager : {errors}");
                }
                if (!await userManager.IsInRoleAsync(user, "CampaignManager"))
                {
                    var roleResult = await userManager.AddToRoleAsync(user, "CampaignManager");
                    if (!roleResult.Succeeded)
                    {
                        var errors = new StringBuilder();
                        foreach (var error in roleResult.Errors)
                        {
                            errors.AppendLine(error.Description);
                        }

                        throw new InvalidOperationException(
                            $"Failed to assign CampaignManager role: {errors}");
                    }
                }
            }
        }
    }
}