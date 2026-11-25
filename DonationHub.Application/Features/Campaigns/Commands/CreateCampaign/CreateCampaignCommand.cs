using DonationHub.Application.Features.Campaigns.Requests;
using DonationHub.Application.Features.Campaigns.Responses;
using DonationHub.Domain.Common.Results.Abstractions;
using MediatR;

namespace DonationHub.Application.Features.Campaigns.Commands.CreateCampaign
{
    public sealed record CreateCampaignCommand(CreateCampaignRequest CreateCampaignRequest)
        : IRequest<Result<CampaignListDto>>;

}
