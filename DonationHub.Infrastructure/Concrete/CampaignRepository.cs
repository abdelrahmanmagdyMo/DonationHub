using DonationHub.Application.Contracts;
using DonationHub.Domain.Campaigns;
using DonationHub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DonationHub.Infrastructure.Concrete
{
    public class CampaignRepository(AppDbContext context) : IGenericRepository<Campaign, Guid>, ICampaignRepository
    {
        public async Task AddAsync(Campaign entity, CancellationToken ct = default)
        {
            await context.Campaigns.AddAsync(entity, ct);
        }

        public async Task<IReadOnlyList<Campaign>> GetAllAsync(CancellationToken ct = default)
        {
            return await context.Campaigns.IgnoreQueryFilters().ToListAsync(ct);
        }

        public async Task<Campaign?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await context.Campaigns.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public void Update(Campaign entity)
        {
            context.Campaigns.Update(entity);
        }
    }
}
