using DonationHub.Domain.Campaigns;
using DonationHub.Domain.Interfaces;
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
            return await context.Campaigns.IgnoreQueryFilters().AsNoTracking().ToListAsync(ct);
        }

        public async Task<Campaign?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await context.Campaigns.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<bool> HasDonationsAsync(Guid campaignId, CancellationToken ct)
        => await context.Donations.AnyAsync(c => c.CampaignId == campaignId, ct);

        public void SetOriginalRowVersion(Campaign campaign, string RowVersion)
        {
            context.Entry(campaign).Property(c => c.RowVersion).OriginalValue = Convert.FromBase64String(RowVersion);
        }

        public void Update(Campaign entity)
        {

            context.Campaigns.Update(entity);
        }
    }
}
