using DonationHub.Application.Contracts;
using DonationHub.Domain.IdempotencyRecords;
using DonationHub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DonationHub.Infrastructure.Concrete
{
    public class IdempotencyRecordRepository(AppDbContext context) :
        IGenericRepository<IdempotencyRecord, string>, IIdempotencyRecordRepository
    {
        public async Task AddAsync(IdempotencyRecord entity, CancellationToken ct = default)
        {
            await context.AddAsync(entity, ct);
        }

        public async Task<IReadOnlyList<IdempotencyRecord>> GetAllAsync(CancellationToken ct = default)
        {
            return await context.IdempotencyRecords.ToListAsync(ct);
        }

        public async Task<IdempotencyRecord?> GetByIdAsync(string key, CancellationToken ct = default)
        {
            return await context.IdempotencyRecords.FirstOrDefaultAsync(x => x.Key == key, ct);
        }



        public void Update(IdempotencyRecord entity)
        {
            context.IdempotencyRecords.Update(entity);
        }
    }
}
