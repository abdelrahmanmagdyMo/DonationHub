using DonationHub.Application.Features.Campaigns.Responses;
using DonationHub.Domain.Campaigns;
using DonationHub.Domain.ValueObjects;

namespace DonationHub.Application.Features.Campaigns.Mappers
{
    public static class CampaignMapper
    {
        public static CampaignListDto ToCampaignListDto(this Campaign campaign, string lang)
        {
            ArgumentNullException.ThrowIfNull(campaign);
            var isArabic = lang == "ar";
            return new CampaignListDto
            {
                Id = campaign.Id,
                Title = isArabic ? campaign.Title.Arabic : campaign.Title.English,
                Description = isArabic ? campaign.Description.Arabic : campaign.Description.English,
                CategoryId = campaign.CategoryId,
                GoalAmount = campaign.GoalAmount.ToMoneyDto(),
                RaisedAmount = campaign.RaisedAmount.ToMoneyDto(),
                EndDate = campaign.EndDate,
                Status = campaign.Status,
                Version = Convert.ToBase64String(campaign.RowVersion)
            };
        }
        public static IEnumerable<CampaignListDto> ToCampaignListDtos(this IEnumerable<Campaign> campaigns, string lang)
        {
            return [.. campaigns.Select(c => c.ToCampaignListDto(lang))];
        }
        private static MoneyDto ToMoneyDto(this Money money)
        {
            ArgumentNullException.ThrowIfNull(money);
            return new MoneyDto
            {
                Amount = money.Amount,
                Currency = money.Currency
            };
        }
    }
}
