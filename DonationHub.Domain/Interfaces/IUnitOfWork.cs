using DonationHub.Domain.Campaigns;
using DonationHub.Domain.Categories;
using DonationHub.Domain.Donations;
using DonationHub.Domain.IdempotencyRecords;
using DonationHub.Domain.Payments;
using DonationHub.Domain.ProcessedStripeEvents;

namespace DonationHub.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepository { get; }
        ICampaignRepository CampaignRepository { get; }
        IDonationRepository DonationRepository { get; }
        IIdempotencyRecordRepository IdempotencyRecordRepository { get; }
        IPaymentAttemptRepository PaymentAttemptRepository { get; }
        IProcessedStripeEventRepository ProcessedStripeEventRepository { get; }
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
