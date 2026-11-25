using DonationHub.Domain.Interfaces;

namespace DonationHub.Domain.Campaigns
{
    public interface ICampaignRepository : IGenericRepository<Campaign, Guid>
    {
        Task<bool> HasDonationsAsync(Guid campaignId, CancellationToken ct);
        void SetOriginalRowVersion(Campaign campaign, string RowVersion);
    }
}
