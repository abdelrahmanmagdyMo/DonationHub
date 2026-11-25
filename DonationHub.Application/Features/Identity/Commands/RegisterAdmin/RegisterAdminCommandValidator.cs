using FluentValidation;

namespace DonationHub.Application.Features.Identity.Commands.RegisterAdmin
{
    public class RegisterAdminCommandValidator : AbstractValidator<RegisterAdminCommand>
    {
        private static readonly string[] AllowedRoles = ["Admin", "CampaignManager"];

        public RegisterAdminCommandValidator()
        {
            RuleFor(c => c.RegisterDto.Email)
                .NotEmpty().WithMessage("Email Cannot Be Empty")
                .EmailAddress().WithMessage("Email Must Be In Valid Format");

            RuleFor(c => c.RegisterDto.FullName)
                .NotEmpty().WithMessage("Name Cannot Be Empty");

            RuleFor(c => c.RegisterDto.Role).NotEmpty().WithMessage("Role Cannot Be Empty")
                .Must(r => AllowedRoles.Contains(r)).WithMessage("ole Must Be Either Admin Or CampaignManager");
        }
    }
}
