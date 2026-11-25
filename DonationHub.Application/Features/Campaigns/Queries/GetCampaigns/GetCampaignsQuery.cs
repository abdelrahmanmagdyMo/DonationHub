using DonationHub.Application.Features.Campaigns.Responses;
using DonationHub.Domain.Common.Results.Abstractions;
using MediatR;

namespace DonationHub.Application.Features.Campaigns.Queries.GetCampaigns
{
    public sealed record class GetCampaignsQuery(string Lang) : IRequest<Result<IEnumerable<CampaignListDto>>>;

}
