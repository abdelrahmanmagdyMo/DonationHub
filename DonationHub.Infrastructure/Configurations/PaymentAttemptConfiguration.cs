using DonationHub.Domain.Donations;
using DonationHub.Domain.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DonationHub.Infrastructure.Configurations
{
    public class PaymentAttemptConfiguration : IEntityTypeConfiguration<PaymentAttempt>
    {
        public void Configure(EntityTypeBuilder<PaymentAttempt> builder)
        {
            builder.ToTable("PaymentAttempts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.HasOne<Donation>().WithMany().HasForeignKey(x => x.DonationId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.StripeCheckoutSessionId).IsRequired();
            builder.Property(x => x.StripePaymentIntentId).IsRequired(false);
            builder.OwnsOne(x => x.Amount, money =>
            {
                money.Property(e => e.Amount).HasPrecision(18, 2).IsRequired();
                money.Property(e => e.Currency).HasMaxLength(3).IsRequired();
            });
            builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(20).IsRequired();
            builder.Property(x => x.FailureCode).IsRequired(false);
            builder.HasQueryFilter(x => !x.IsDeleted);

        }
    }
}
