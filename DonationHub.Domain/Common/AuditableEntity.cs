namespace DonationHub.Domain.Common
{
    public abstract class AuditableEntity(Guid id) : BaseEntity(id)
    {
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }
    }
}
