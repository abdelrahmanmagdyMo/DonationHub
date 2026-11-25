using FluentValidation;

namespace DonationHub.Application.Features.Campaigns.Commands.CreateCampaign
{
    public sealed class CreateCampaignValidator : AbstractValidator<CreateCampaignCommand>
    {
        public CreateCampaignValidator(TimeProvider timeProvider)
        {

            RuleFor(c => c.CreateCampaignRequest.UserId).NotNull().WithMessage("User Id Cannot Be Null")
                .NotEmpty().WithMessage("User Id Cannot Be Empty");

            RuleFor(c => c.CreateCampaignRequest.CategoryId).NotNull().WithMessage("Category Id Cannot Be Null")
                .NotEmpty().WithMessage("Category Id Cannot Be Empty");

            RuleFor(c => c.CreateCampaignRequest.TitleAr).NotNull().WithMessage("Arabic Title Cannot Be Null")
                .NotEmpty().WithMessage("Arabic Title Cannot Be Empty")
                .MaximumLength(100).WithMessage("Arabic Title Cannot Exceed 100 Letters");

            RuleFor(c => c.CreateCampaignRequest.TitleEn).NotNull().WithMessage("English Title Cannot Be Null")
                .NotEmpty().WithMessage("Arabic Description Cannot Be Empty")
                .MaximumLength(100).WithMessage("Arabic Description Cannot Exceed 100 Letters");

            RuleFor(c => c.CreateCampaignRequest.DescriptionAr).NotNull().WithMessage("Arabic Description Cannot Be Null")
                .NotEmpty().WithMessage("Arabic Description Cannot Be Empty")
                .MaximumLength(100).WithMessage("Arabic Description Cannot Exceed 255 Letters");

            RuleFor(c => c.CreateCampaignRequest.DescriptionEn).NotNull().WithMessage("English Description Cannot Be Null")
                .NotEmpty().WithMessage("English Description Cannot Be Empty")
                .MaximumLength(100).WithMessage("English Description Cannot Exceed 255 Letters");

            RuleFor(c => c.CreateCampaignRequest.GoalAmount.Currency).NotNull().WithMessage("Currency Cannot Be Null")
                .NotEmpty().WithMessage("Currency Cannot Be Empty");
            RuleFor(c => c.CreateCampaignRequest.GoalAmount.Amount).NotEmpty().WithMessage("Amount Cannot Be Empty")
                .GreaterThan(0).WithMessage("Amount Must Be Greater Than Zero .")
                .PrecisionScale(18, 2, ignoreTrailingZeros: true);

            RuleFor(c => c.CreateCampaignRequest.EndDate).NotNull().WithMessage("End Date Cannot Be Null")
                .Must(date => date > timeProvider.GetUtcNow())
                .WithMessage("End Date Must Be In Future");
        }
    }
}
