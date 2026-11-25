using FluentValidation;

namespace DonationHub.Application.Features.Identity.Commands.Register
{
    public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(e => e.RegisterDto.Email).NotNull().WithMessage("Email Cannot Be Null")
            .NotEmpty().WithMessage("Email Cannot Be Empty")
            .EmailAddress().WithMessage("Email Must Be In A Valid Format");

            RuleFor(e => e.RegisterDto.FullName).NotNull().WithMessage("Full Name Cannot Be Null")
            .NotEmpty().WithMessage("Full Name Cannot Be Empty");

            RuleFor(e => e.RegisterDto.Password).NotNull().WithMessage("Password Cannot Be Null")
            .NotEmpty().WithMessage("Password Cannot Be Empty");

            RuleFor(e => e.RegisterDto.PrefferedLanguage)
            .IsInEnum().WithMessage("PrefferedLanguage is not a valid language");
        }
    }
}
