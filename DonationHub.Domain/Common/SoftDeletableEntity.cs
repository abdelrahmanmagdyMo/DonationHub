using DonationHub.Domain.Common.Contracts;

namespace DonationHub.Domain.Common
{
    public abstract class SoftDeletableEntity(Guid id) : AuditableEntity(id),
        ISoftDeletable
    {
        public bool IsDeleted { get; private set; }

        public DateTimeOffset? DeletedAtUtc { get; private set; }

        public void Delete(TimeProvider timeProvider)
        {
            if (IsDeleted) return;
            IsDeleted = true;
            DeletedAtUtc = timeProvider.GetUtcNow();
        }

        public void UndoDelete()
        {
            IsDeleted = false;
            DeletedAtUtc = null;
        }
    }
}
