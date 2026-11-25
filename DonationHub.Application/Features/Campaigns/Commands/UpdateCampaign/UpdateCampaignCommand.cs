using DonationHub.Application.Features.Campaigns.Requests;
using DonationHub.Application.Features.Campaigns.Responses;
using DonationHub.Domain.Common.Results.Abstractions;
using MediatR;

namespace DonationHub.Application.Features.Campaigns.Commands.UpdateCampaign
{
    public sealed record UpdateCampaignCommand(UpdateCampaignRequest UpdateCampaignRequest)
    : IRequest<Result<CampaignListDto>>;
}
