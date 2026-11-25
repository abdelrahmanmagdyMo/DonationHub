using DonationHub.Domain.ValueObjects;

namespace DonationHub.Application.Features.Campaigns.Requests
{
    public sealed record UpdateCampaignRequest
    (
        string Lang,
        Guid Id,
        string TitleAr,
        string TitleEn,
        string DescriptionAr,
        string DescriptionEn,
        Guid CategoryId,
        DateTime EndDate,
        Money GoalAmount,
        string RowVersion);
}
