
namespace DonationHub.Domain.Common.Contracts
{
    public interface ISoftDeletable
    {
        public bool IsDeleted { get; }
        public DateTimeOffset? DeletedAtUtc { get; }
        public void Delete(TimeProvider timeProvider);
        public void UndoDelete();

    }
}

