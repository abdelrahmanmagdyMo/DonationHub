using DonationHub.Application.Contracts;
using DonationHub.Domain.Categories;
using DonationHub.Infrastructure.Persistence;

namespace DonationHub.Infrastructure.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public ICategoryRepository CategoryRepository { get; private set; }
        public ICampaignRepository CampaignRepository { get; private set; }
        public IDonationRepository DonationRepository { get; private set; }
        public IIdempotencyRecordRepository IdempotencyRecordRepository { get; private set; }
        public IPaymentAttemptRepository PaymentAttemptRepository { get; private set; }
        public IProcessedStripeEventRepository ProcessedStripeEventRepository { get; private set; }

        public IDeletableRepository<Category, Guid> DeletableCategoryRepository { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            CategoryRepository = new CategoryRepository(context);
            CampaignRepository = new CampaignRepository(context);
            DonationRepository = new DonationRepository(context);
            IdempotencyRecordRepository = new IdempotencyRecordRepository(context);
            PaymentAttemptRepository = new PaymentAttemptRepository(context);
            ProcessedStripeEventRepository = new ProcessedStripeEventRepository(context);
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public Task<int> SaveChangesAsync(CancellationToken ct = default)
            => _context.SaveChangesAsync(ct);
    }
}
