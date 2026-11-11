using DonationHub.Domain.IdempotencyRecords;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DonationHub.Infrastructure.Configurations
{
    public class IdempotencyRecordConfiguration : IEntityTypeConfiguration<IdempotencyRecord>
    {
        public void Configure(EntityTypeBuilder<IdempotencyRecord> builder)
        {
            builder.ToTable("IdempotencyRecords");
            builder.HasKey(x => new { x.UserId, x.Key });
            builder.Property(x => x.Key).HasMaxLength(IdempotencyRecord.KeyMaxLength).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.ResponseStatusCode).IsRequired();
            builder.Property(x => x.ResponseBody).IsRequired();
            builder.Property(x => x.RequestHash).HasMaxLength(IdempotencyRecord.RequestHashMaxLength).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.ExpiresAt).IsRequired();

        }
    }
}
