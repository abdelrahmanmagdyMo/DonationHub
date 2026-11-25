using DonationHub.Application.Features.Campaigns.Responses;
using DonationHub.Domain.Common.Results.Abstractions;
using MediatR;

namespace DonationHub.Application.Features.Campaigns.Queries.GetCampaignById
{
    public sealed record GetCampaignByIdQuery(Guid Id, string Lang) : IRequest<Result<CampaignListDto>>;

}
