
using DonationHub.Domain.Persistence.Identity;
using DonationHub.Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DonationHub.Infrastructure.Configurations
{
    public sealed class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("AspNetUsers");

            builder.Property(x => x.FullName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.PreferredLanguage).IsRequired()
                .HasConversion<string>().HasMaxLength(2).HasDefaultValue(PrefferedLanguage.EN);
        }
    }
}
