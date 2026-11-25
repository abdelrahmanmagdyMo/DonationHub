namespace DonationHub.Application.Features.Campaigns.Responses
{
    public sealed record CampaignAdminDto
        (
        Guid Id,
        string TitleAr,
        string TitleEn,
        string DescriptionAr,
        string DescriptionEn,
        MoneyDto GoalAmount,
        MoneyDto RaisedAmount,
        DateTimeOffset EndDate,
        string? BannerFileName,
        string Status,
        Guid CategoryId,
        Guid CreatedByUserId,
        byte[] RowVersion
        );

}
