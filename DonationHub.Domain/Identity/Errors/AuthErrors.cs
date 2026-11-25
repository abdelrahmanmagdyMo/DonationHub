using DonationHub.Domain.Common.Results;

namespace DonationHub.Domain.Identity.Errors
{
    public static class AuthErrors
    {
        public static Error Validation => new("Auth.Validation", "Input validation failure", ErrorType.Validation);
        public static Error InvalidCredentials => new("Auth.InvalidCredentials", "Wrong email or password", ErrorType.Validation);
        public static Error EmailAlreadyExists => new("Auth.EmailAlreadyExists", "Registration with an existing email", ErrorType.Conflict);
        public static Error RefreshTokenInvalid => new("Auth.RefreshTokenInvalid", "Refresh token unknown, expired or revoked", ErrorType.Conflict);
        public static Error AccountDeactivated => new("Auth.AccountDeactivated", "Deactivated user logs in or refreshes", ErrorType.Failure);
    }
}
