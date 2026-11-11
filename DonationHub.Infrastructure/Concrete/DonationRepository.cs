using DonationHub.Application.Contracts;
using DonationHub.Domain.Donations;
using DonationHub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DonationHub.Infrastructure.Concrete
{
    public class DonationRepository(AppDbContext context) : IGenericRepository<Donation, Guid>
        , IDonationRepository
    {
        public async Task AddAsync(Donation entity, CancellationToken ct = default)
        {
            await context.Donations.AddAsync(entity, ct);
        }

        public async Task<IReadOnlyList<Donation>> GetAllAsync(CancellationToken ct = default)
        {
            return await context.Donations.IgnoreQueryFilters().ToListAsync(ct);
        }


        public async Task<Donation?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await context.Donations.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public void Update(Donation entity)
        {
            context.Donations.Update(entity);
        }
    }
}
