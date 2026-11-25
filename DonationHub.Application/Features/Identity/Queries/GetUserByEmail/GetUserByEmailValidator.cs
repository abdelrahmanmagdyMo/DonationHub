using FluentValidation;

namespace DonationHub.Application.Features.Identity.Queries.GetUserByEmail
{
    public class GetUserByEmailValidator : AbstractValidator<GetUserByEmailQuery>
    {
        public GetUserByEmailValidator()
        {
            RuleFor(c => c.Email).NotEmpty()
               .WithMessage("Email is required.")
               .EmailAddress().WithMessage("Email Must Be In Valid Format");
        }
    }
}
