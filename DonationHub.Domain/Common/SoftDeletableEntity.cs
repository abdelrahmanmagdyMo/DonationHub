using DonationHub.Domain.Campaigns.Contract;

namespace DonationHub.Domain.Common
{
    public abstract class SoftDeletableEntity(Guid id) : AuditableEntity(id), ISoftDeletable
    {
        public bool IsDeleted { get; private set; }

        public DateTime? DeletedAtUtc { get; private set; }

        public void Delete()
        {
            if (IsDeleted) return;
            IsDeleted = true;
            DeletedAtUtc = DateTime.UtcNow;
        }

        public void UndoDelete()
        {
            IsDeleted = false;
            DeletedAtUtc = null;
        }
    }
}
