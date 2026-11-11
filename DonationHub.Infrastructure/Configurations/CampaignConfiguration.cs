using DonationHub.Domain.Campaigns;
using DonationHub.Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DonationHub.Infrastructure.Configurations
{
    public class CampaignConfiguration : IEntityTypeConfiguration<Campaign>
    {
        public void Configure(EntityTypeBuilder<Campaign> builder)
        {
            builder.ToTable("Campaigns");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.OwnsOne(x => x.Title, title =>
            {
                title.Property(e => e.Arabic).HasMaxLength(100).IsRequired();
                title.Property(e => e.English).HasMaxLength(100).IsRequired();
            });
            builder.OwnsOne(x => x.Description, desc =>
            {
                desc.Property(e => e.Arabic).HasMaxLength(255).IsRequired();
                desc.Property(e => e.English).HasMaxLength(255).IsRequired();
            });
            builder.OwnsOne(x => x.GoalAmount, money =>
            {
                money.Property(c => c.Amount).HasPrecision(18, 2).IsRequired();
                money.Property(c => c.Currency).HasMaxLength(3).IsRequired();
            });
            builder.OwnsOne(x => x.RaisedAmount, money =>
            {
                money.Property(c => c.Amount).HasPrecision(18, 2).IsRequired();
                money.Property(c => c.Currency).HasMaxLength(3).IsRequired();
            });
            builder.Property(x => x.CategoryId).IsRequired();
            builder.HasOne(x => x.Category).WithMany(x => x.Campaigns).HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.BannerFileName).HasMaxLength(255).IsRequired(false);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt).IsRequired();
            builder.Property(x => x.EndDate).IsRequired();
            builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(20);
            builder.Property(x => x.RowVersion).IsConcurrencyToken();

            builder.HasOne<ApplicationUser>().WithMany().HasForeignKey(x => x.CreatedByUserId).OnDelete(DeleteBehavior.Restrict);
            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
