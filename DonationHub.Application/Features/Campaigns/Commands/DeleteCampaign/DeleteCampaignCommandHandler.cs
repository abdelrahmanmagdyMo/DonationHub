using DonationHub.Domain.Campaigns;
using DonationHub.Domain.Common.Results;
using DonationHub.Domain.Interfaces;
using MediatR;
using CampaignErrors = DonationHub.Domain.Campaigns.Errors;
namespace DonationHub.Application.Features.Campaigns.Commands.DeleteCampaign
{
    public sealed class DeleteCampaignCommandHandler(TimeProvider timeProvider, ICampaignRepository campaignRepository, IUnitOfWork unitOfWork)
        : IRequestHandler<DeleteCampaignCommand, Result>
    {
        public async Task<Result> Handle(DeleteCampaignCommand request, CancellationToken ct)
        {
            var campaign = await campaignRepository.GetByIdAsync(request.Id, ct);
            if (campaign is null) return Result.Failure(CampaignErrors.CampaignErrors.NotFound);
            var campaignWithDonation = await campaignRepository.HasDonationsAsync(request.Id, ct);
            if (campaignWithDonation != null) return Result.Failure(CampaignErrors.CampaignErrors.CampaignHasDonations);
            campaign.Delete(timeProvider);
            await unitOfWork.SaveChangesAsync();
            return Result.Success();
        }
    }
}
