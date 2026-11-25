using DonationHub.Domain.ValueObjects;

namespace DonationHub.Application.Features.Campaigns.Requests
{
    public sealed record CreateCampaignRequest(string Lang,
        string TitleAr,
        string TitleEn,
        string DescriptionAr,
        string DescriptionEn,
        Money GoalAmount,
        DateTimeOffset EndDate,
        Guid CategoryId,
        Guid UserId);

}
