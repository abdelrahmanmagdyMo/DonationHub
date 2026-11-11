using DonationHub.Infrastructure.Options;
using DonationHub.Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DonationHub.Infrastructure.Persistence.Seeding
{
    public static class SeedingExtension
    {
        public static async Task SeedAdminAsync(this IServiceProvider service)
        {
            using var scope = service.CreateScope();
            var sp = scope.ServiceProvider;
            var options = sp.GetRequiredService<IOptions<SeedAdminOptions>>().Value
; var userManager = sp.GetRequiredService<UserManager<ApplicationUser>>();
            var timeProvider = sp.GetRequiredService<TimeProvider>();
            await AdminSeeder.SeedAdmin(options, userManager, timeProvider);
        }
        public static async Task SeedDonorAsync(this IServiceProvider service)
        {
            using var scope = service.CreateScope();
            var sp = scope.ServiceProvider;
            var options = sp.GetRequiredService<IOptions<SeedDonorOptions>>().Value
; var userManager = sp.GetRequiredService<UserManager<ApplicationUser>>();

            await DonorSeeder.SeedDonor(options, userManager, DateTimeOffset.UtcNow);
        }
        public static async Task SeedManagerAsync(this IServiceProvider service)
        {
            using var scope = service.CreateScope();
            var sp = scope.ServiceProvider;
            var options = sp.GetRequiredService<IOptions<CampaignManagerSeedOptions>>().Value
; var userManager = sp.GetRequiredService<UserManager<ApplicationUser>>();

            await ManagerSeeder.SeedManager(options, userManager, DateTimeOffset.UtcNow);
        }
    }
}
