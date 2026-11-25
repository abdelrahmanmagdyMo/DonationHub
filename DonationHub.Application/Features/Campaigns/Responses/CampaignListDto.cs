using DonationHub.Domain.Campaigns.Enums;

namespace DonationHub.Application.Features.Campaigns.Responses
{
    public class CampaignListDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public MoneyDto GoalAmount { get; set; }
        public MoneyDto RaisedAmount { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public CampaignStatus Status { get; set; }
        public Guid CategoryId { get; set; }
        public string Version { get; set; }
    }
}