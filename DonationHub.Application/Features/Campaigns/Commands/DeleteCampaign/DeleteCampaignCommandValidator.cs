using FluentValidation;

namespace DonationHub.Application.Features.Campaigns.Commands.DeleteCampaign
{
    public sealed class DeleteCampaignCommandValidator : AbstractValidator<DeleteCampaignCommand>
    {
        public DeleteCampaignCommandValidator()
        {
            RuleFor(c => c.Id).NotNull().WithMessage("Id Cannot Be Null")
                   .NotEmpty().WithMessage("Id Cannot Be Empty");
        }
    }
}
