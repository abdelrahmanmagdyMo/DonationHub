using DonationHub.Domain.Donations;
using DonationHub.Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DonationHub.Infrastructure.Configurations
{
    public class DonationConfiguration : IEntityTypeConfiguration<Donation>
    {
        public void Configure(EntityTypeBuilder<Donation> builder)
        {
            builder.ToTable("Donations");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.OwnsOne(x => x.Amount, money =>
            {
                money.Property(e => e.Amount).HasPrecision(18, 2).IsRequired();
                money.Property(e => e.Currency).HasMaxLength(3).IsRequired();
            });
            builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(20);
            builder.HasOne(x => x.Campaign).WithMany(x => x.Donations).HasForeignKey(x => x.CampaignId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<ApplicationUser>().WithMany().HasForeignKey(x => x.DonorUserId).
                OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.ReceiptFileName).HasMaxLength(255).IsRequired(false);
            builder.Property(x => x.ReceiptNumber).HasMaxLength(255).IsRequired(false);
            builder.Property(x => x.FailureReason).HasMaxLength(255).IsRequired(false);
            builder.HasQueryFilter(d => !d.Campaign.IsDeleted);
        }
    }
}
