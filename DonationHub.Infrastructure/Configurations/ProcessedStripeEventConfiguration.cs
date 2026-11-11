using DonationHub.Domain.ProcessedStripeEvent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DonationHub.Infrastructure.Configurations
{
    public class ProcessedStripeEventConfiguration : IEntityTypeConfiguration<ProcessedStripeEvent>
    {
        public void Configure(EntityTypeBuilder<ProcessedStripeEvent> builder)
        {
            builder.ToTable("ProcessedStripeEvents");
            builder.HasKey(x => x.EventId);
            builder.Property(x => x.EventId).HasMaxLength(ProcessedStripeEvent.EventIdMaxLength).IsRequired();
            builder.Property(x => x.Type).HasMaxLength(ProcessedStripeEvent.TypeMaxLength).IsRequired();
            builder.Property(x => x.ProcessedAt).IsRequired();

        }
    }
}
