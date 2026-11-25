using DonationHub.Domain.Campaigns.Enums;

namespace DonationHub.Application.Features.Campaigns.Responses
{
    public sealed record CampaignDetailsDto(
    Guid Id,
    string Title,
    string Description,
    MoneyDto GoalAmount,
    MoneyDto RaisedAmount,
    DateTimeOffset EndDate,
    CampaignStatus Status,
    Guid CategoryId,
    Guid CreatedByUserId,
    decimal ProgressPercent
    );
}
