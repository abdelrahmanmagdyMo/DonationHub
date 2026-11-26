using DonationHub.Application.Contracts;
using DonationHub.Application.Features.Campaigns.Mappers;
using DonationHub.Application.Features.Campaigns.Responses;
using DonationHub.Domain.Campaigns;
using DonationHub.Domain.Common.Results.Abstractions;
using DonationHub.Domain.Interfaces;
using DonationHub.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CampaignErrors = DonationHub.Domain.Campaigns.Errors;
namespace DonationHub.Application.Features.Campaigns.Commands.UpdateCampaign
{
    public sealed class UpdateCampaignCommandHandler(ICurrentUser currentUser, ILanguageHelper languageHelper, ICampaignRepository campaignRepository, IUnitOfWork unitOfWork, TimeProvider timeProvider) : IRequestHandler<UpdateCampaignCommand, Result<CampaignListDto>>
    {
        public async Task<Result<CampaignListDto>> Handle(UpdateCampaignCommand dto, CancellationToken ct)
        {
            if (currentUser.UserId is not { } userId)
                return Result<CampaignListDto>.Failure(CampaignErrors.CampaignErrors.Validation);

            var request = dto.UpdateCampaignRequest;
            var campaign = await campaignRepository.GetByIdAsync(request.Id, ct);
            if (campaign is null) return Result<CampaignListDto>.Failure(CampaignErrors.CampaignErrors.NotFound);

            if (campaign.CreatedByUserId != currentUser.UserId && !currentUser.IsAdmin)
                return Result<CampaignListDto>.Failure(CampaignErrors.CampaignErrors.Validation);

            var campaignResult = campaign.Update(LocalizedText.Create(request.TitleAr, request.TitleEn), LocalizedText.Create(
                request.DescriptionAr, request.DescriptionEn), Money.Create(request.GoalAmount.Amount, request.GoalAmount.Currency),
                request.CategoryId, request.EndDate, timeProvider, Convert.FromBase64String(request.RowVersion));
            if (campaignResult.IsFailure) return Result<CampaignListDto>.Failure(campaignResult.Error);
            campaignRepository.SetOriginalRowVersion(campaign, request.RowVersion);
            campaignRepository.Update(campaign);
            try
            {
                await unitOfWork.SaveChangesAsync(ct);
                var campaignResponse = campaign.ToCampaignListDto(languageHelper.GetCurrentLanguage());
                return Result<CampaignListDto>.Success(campaignResponse);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Result<CampaignListDto>.Failure(CampaignErrors.CampaignErrors.ConcurrencyConflict);

            }

        }
    }
}
