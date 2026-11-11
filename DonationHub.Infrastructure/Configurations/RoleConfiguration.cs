using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DonationHub.Infrastructure.Configurations
{
    public sealed class RoleConfiguration : IEntityTypeConfiguration<IdentityRole<Guid>>
    {
        public static readonly Guid AdminId = Guid.Parse("758B512D-A3A5-495F-B687-2CFB1299204D");
        public static readonly Guid CampaignManagerId = Guid.Parse("9761F49A-8DC2-4BD4-A186-AAEF0274C9F3");
        public static readonly Guid DonorId = Guid.Parse("D06E57F5-0657-4EAE-8DE0-13FB8F7B9881");
        public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder)
        {
            builder.HasData(
                new IdentityRole<Guid>
                {
                    Id = AdminId,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = AdminId.ToString()
                },
                new IdentityRole<Guid>
                {
                    Id = CampaignManagerId,
                    Name = "CampaignManager",
                    NormalizedName = "CAMPAIGNMANAGER",
                    ConcurrencyStamp = CampaignManagerId.ToString()
                },
                new IdentityRole<Guid>
                {
                    Id = DonorId,
                    Name = "Donor",
                    NormalizedName = "DONOR",
                    ConcurrencyStamp = DonorId.ToString()
                }
                );
        }
    }
}









