using FluentValidation;

namespace DonationHub.Application.Features.Identity.Commands.Logout
{
    public class LogoutCommandValidator : AbstractValidator<LogoutCommand>
    {
        public LogoutCommandValidator()
        {
            RuleFor(c => c.RefreshToken).NotEmpty().WithMessage("Refresh Token Cannot Be Empty");
        }
    }
}
