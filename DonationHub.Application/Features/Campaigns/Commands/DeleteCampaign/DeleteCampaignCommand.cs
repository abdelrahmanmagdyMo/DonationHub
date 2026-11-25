using DonationHub.Domain.Common.Results;
using MediatR;

namespace DonationHub.Application.Features.Campaigns.Commands.DeleteCampaign
{
    public sealed record DeleteCampaignCommand(Guid Id) : IRequest<Result>;
}
