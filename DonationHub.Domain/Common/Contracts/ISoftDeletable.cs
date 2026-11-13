
namespace DonationHub.Domain.Common.Contracts
{
    public interface ISoftDeletable
    {
        public bool IsDeleted { get; }
        public DateTime? DeletedAtUtc { get; }
        public void Delete();
        public void UndoDelete();

    }
}

