using FluentValidation;

namespace DonationHub.Application.Features.Identity.Commands.Refresh
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(x => x.RefreshToken).NotEmpty();

        }
    }
}
