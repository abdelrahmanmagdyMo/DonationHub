using FluentValidation;

namespace DonationHub.Application.Features.Categories.Commands.DeleteCategory
{
    public sealed class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(c => c.Id).NotNull().WithMessage("Id Cannot Be Null")
                .NotEmpty().WithMessage("Id Cannot Be Empty");
        }
    }
}
