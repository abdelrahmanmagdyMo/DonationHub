using FluentValidation;

namespace DonationHub.Application.Features.Identity.Queries.GetUserById
{
    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator()
        {
            RuleFor(c => c.UserId).NotEmpty()
                .WithMessage("User ID is required.");
        }
    }
}
