using DonationHub.Application.Features.Campaigns.Mappers;
using DonationHub.Application.Features.Campaigns.Responses;
using DonationHub.Domain.Campaigns;
using DonationHub.Domain.Common.Results.Abstractions;
using MediatR;

namespace DonationHub.Application.Features.Campaigns.Queries.GetCampaigns
{
    public sealed class GetCampaignsQueryHandler(ICampaignRepository campaignRepository)
        : IRequestHandler<GetCampaignsQuery, Result<IEnumerable<CampaignListDto>>>
    {
        public async Task<Result<IEnumerable<CampaignListDto>>> Handle(GetCampaignsQuery request, CancellationToken ct)
        {
            var campaigns = await campaignRepository.GetAllAsync(ct);
            return
                Result<IEnumerable<CampaignListDto>>.Success(campaigns.ToCampaignListDtos(request.Lang));
        }
    }
}
