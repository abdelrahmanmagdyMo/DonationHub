using DonationHub.Application.Contracts;
using DonationHub.Application.Features.Campaigns.Mappers;
using DonationHub.Application.Features.Campaigns.Responses;
using DonationHub.Domain.Campaigns;
using DonationHub.Domain.Common.Results.Abstractions;
using MediatR;
using CampaignErrors = DonationHub.Domain.Campaigns.Errors;
namespace DonationHub.Application.Features.Campaigns.Queries.GetCampaignById
{
    public sealed class GetCampaignByIdQueryHandler(ILanguageHelper languageHelper, ICampaignRepository campaignRepository)
        : IRequestHandler<GetCampaignByIdQuery, Result<CampaignListDto>>
    {
        public async Task<Result<CampaignListDto>> Handle(GetCampaignByIdQuery request, CancellationToken ct)
        {
            var campaign = await campaignRepository.GetByIdAsync(request.Id, ct);
            if (campaign is null) return Result<CampaignListDto>.Failure(CampaignErrors.CampaignErrors.NotFound);
            return Result<CampaignListDto>.Success(campaign.ToCampaignListDto(languageHelper.GetCurrentLanguage()));
        }
    }
}
