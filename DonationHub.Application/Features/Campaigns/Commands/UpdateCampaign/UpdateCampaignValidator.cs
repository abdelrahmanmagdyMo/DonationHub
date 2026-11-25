using FluentValidation;


namespace DonationHub.Application.Features.Campaigns.Commands.UpdateCampaign
{
    public sealed class UpdateCampaignValidator : AbstractValidator<UpdateCampaignCommand>
    {
        public UpdateCampaignValidator(TimeProvider timeProvider)
        {
            RuleFor(c => c.UpdateCampaignRequest.Id).NotNull().WithMessage("Id Cannot Be Null")
                    .NotEmpty().WithMessage("Id Cannot Be Empty");

            RuleFor(c => c.UpdateCampaignRequest.CategoryId).NotNull().WithMessage("Category Id Cannot Be Null")
                    .NotEmpty().WithMessage("Category Id Cannot Be Empty");

            RuleFor(c => c.UpdateCampaignRequest.TitleAr).NotNull().WithMessage("Arabic Title Cannot Be Null")
                    .NotEmpty().WithMessage("Arabic Title Cannot Be Empty")
                    .MaximumLength(100).WithMessage("Arabic Title Cannot Exceed 100 Letters");

            RuleFor(c => c.UpdateCampaignRequest.TitleEn).NotNull().WithMessage("English Title Cannot Be Null")
                    .NotEmpty().WithMessage("English Title Cannot Be Empty")
                    .MaximumLength(100).WithMessage("English Title Cannot Exceed 100 Letters");

            RuleFor(c => c.UpdateCampaignRequest.DescriptionAr).NotNull().WithMessage("Arabic Description Cannot Be Null")
                    .NotEmpty().WithMessage("Arabic Title Cannot Be Empty")
                    .MaximumLength(100).WithMessage("English Title Cannot Exceed 255 Letters");

            RuleFor(c => c.UpdateCampaignRequest.DescriptionEn).NotNull().WithMessage("English Description Cannot Be Null")
                    .NotEmpty().WithMessage("English Description Cannot Be Empty")
                    .MaximumLength(100).WithMessage("English Description Cannot Exceed 255 Letters");

            RuleFor(c => c.UpdateCampaignRequest.GoalAmount.Currency).NotNull().WithMessage("Currency Cannot Be Null")
                    .NotEmpty().WithMessage("Currency Cannot Be Empty");
            RuleFor(c => c.UpdateCampaignRequest.GoalAmount.Amount).NotEmpty().WithMessage("Amount Cannot Be Empty")
                    .GreaterThan(0).WithMessage("Amount Must Be Greater Than Zero .")
                    .PrecisionScale(18, 2, ignoreTrailingZeros: true);

            RuleFor(c => c.UpdateCampaignRequest.EndDate).NotNull().WithMessage("End Date Cannot Be Null")
                    .Must(date => date > timeProvider.GetUtcNow())
                    .WithMessage("End Date Must Be In Future");
        }
    }
}
