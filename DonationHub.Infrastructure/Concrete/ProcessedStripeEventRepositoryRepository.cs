using DonationHub.Domain.Interfaces;
using DonationHub.Domain.ProcessedStripeEvents;
using DonationHub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DonationHub.Infrastructure.Concrete
{
    public class ProcessedStripeEventRepository(AppDbContext context) :
        IGenericRepository<ProcessedStripeEvent, string>, IProcessedStripeEventRepository
    {
        public async Task AddAsync(ProcessedStripeEvent entity, CancellationToken ct = default)
        {
            await context.ProcessedStripeEvents.AddAsync(entity, ct);

        }

        public async Task<IReadOnlyList<ProcessedStripeEvent>> GetAllAsync(CancellationToken ct = default)
        {
            return await context.ProcessedStripeEvents.AsNoTracking().ToListAsync(ct);
        }

        public async Task<ProcessedStripeEvent?> GetByIdAsync(string id, CancellationToken ct = default)
        {
            return await context.ProcessedStripeEvents.AsNoTracking().FirstOrDefaultAsync(x => x.EventId == id, ct);
        }

        public void Update(ProcessedStripeEvent entity)
        {
            context.ProcessedStripeEvents.Update(entity);
        }
    }
}
