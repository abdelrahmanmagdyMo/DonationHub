using FluentValidation;

namespace DonationHub.Application.Features.Categories.Commands.CreateCategory
{
    public sealed class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(c => c.NameAr).NotNull().WithMessage("Arabic Name Cannot Be Null")
            .NotEmpty().WithMessage("Arabic Name Cannot Be Empty")
            .MaximumLength(100).WithMessage("Arabic Name Cannot Exceed 100 Letters");

            RuleFor(c => c.NameEn).NotNull().WithMessage("English Name Cannot Be Null")
            .NotEmpty().WithMessage("English Name Cannot Be Empty")
            .MaximumLength(100).WithMessage("English Name Cannot Exceed 100 Letters");

            RuleFor(c => c.DescriptionAr).NotNull().WithMessage("Arabic Description Cannot Be Null")
            .NotEmpty().WithMessage("Arabic Description Cannot Be Empty")
            .MaximumLength(255).WithMessage("Arabic Description Cannot Exceed 255 Letters");

            RuleFor(c => c.DescriptionEn).NotNull().WithMessage("English Description Cannot Be Null")
            .NotEmpty().WithMessage("English Description Cannot Be Empty")
            .MaximumLength(255).WithMessage("English Description Cannot Exceed 255 Letters");
        }
    }
}
