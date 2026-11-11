using DonationHub.Domain.Campaigns;

namespace DonationHub.Application.Contracts
{
    public interface ICampaignRepository : IGenericRepository<Campaign, Guid>
    {
    }
}
