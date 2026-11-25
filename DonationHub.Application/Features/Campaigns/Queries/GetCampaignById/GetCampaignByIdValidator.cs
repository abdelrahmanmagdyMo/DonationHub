using FluentValidation;

namespace DonationHub.Application.Features.Campaigns.Queries.GetCampaignById
{
    public sealed class GetCampaignByIdValidator : AbstractValidator<GetCampaignByIdQuery>
    {
        public GetCampaignByIdValidator()
        {
            RuleFor(c => c.Id).NotNull().WithMessage("Id Cannot Be Null")
                  .NotEmpty().WithMessage("Id Cannot Be Empty");
        }
    }
}
