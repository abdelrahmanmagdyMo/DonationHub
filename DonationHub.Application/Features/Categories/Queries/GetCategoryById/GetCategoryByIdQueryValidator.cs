using FluentValidation;

namespace DonationHub.Application.Features.Categories.Commands.CreateCategory
{
    public sealed class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdQuery>
    {
        public GetCategoryByIdQueryValidator()
        {
            RuleFor(p => p.Id).NotNull().WithMessage("Id Cannot Be Null")
                .NotEmpty().WithMessage("Id Cannot Be Empty");


        }
    }
}
