using DonationHub.Application.Features.Campaigns.Mappers;
using DonationHub.Application.Features.Campaigns.Responses;
using DonationHub.Domain.Campaigns;
using DonationHub.Domain.Common.Results.Abstractions;
using DonationHub.Domain.Interfaces;
using DonationHub.Domain.ValueObjects;
using MediatR;
namespace DonationHub.Application.Features.Campaigns.Commands.CreateCampaign
{
    public sealed class CreateCampaignCommandHandler(ICampaignRepository campaignRepository, IUnitOfWork unitOfWork, TimeProvider timeProvider) : IRequestHandler<CreateCampaignCommand, Result<CampaignListDto>>
    {
        public async Task<Result<CampaignListDto>> Handle(CreateCampaignCommand dto, CancellationToken ct)
        {
            var request = dto.CreateCampaignRequest;
            var campaignResult = Campaign.Create(LocalizedText.Create(request.TitleAr, request.TitleEn), LocalizedText.Create(
                request.DescriptionAr, request.DescriptionEn),
                Money.Create(request.GoalAmount.Amount, request.GoalAmount.Currency), request.CategoryId
                , request.EndDate, timeProvider, request.UserId);
            if (campaignResult.IsFailure) return Result<CampaignListDto>.Failure(campaignResult.Error);
            await campaignRepository.AddAsync(campaignResult.Value, ct);
            await unitOfWork.SaveChangesAsync(ct);
            var campaignResponse = campaignResult.Value.ToCampaignListDto(request.Lang);
            return Result<CampaignListDto>.Success(campaignResponse);
        }
    }
}
