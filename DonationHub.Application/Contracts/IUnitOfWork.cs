using DonationHub.Domain.Categories;

namespace DonationHub.Application.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepository { get; }
        ICampaignRepository CampaignRepository { get; }
        IDonationRepository DonationRepository { get; }
        IIdempotencyRecordRepository IdempotencyRecordRepository { get; }
        IPaymentAttemptRepository PaymentAttemptRepository { get; }
        IProcessedStripeEventRepository ProcessedStripeEventRepository { get; }
        IDeletableRepository<Category, Guid> DeletableCategoryRepository { get; }
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
