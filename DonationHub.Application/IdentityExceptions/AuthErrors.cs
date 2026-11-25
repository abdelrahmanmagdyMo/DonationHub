using DonationHub.Domain.Common.Results;

namespace DonationHub.Application.IdentityExceptions
{
    public static class AuthErrors
    {
        public static Error InvalidCredentials = new("Auth.InvalidCredentials", "INvalid Credentials", ErrorType.Forbidden);
        public static Error EmailAlreadyExists = new("Auth.EmailAlreadyExists", "Email Already Exists", ErrorType.Conflict);
        public static Error RefreshTokenInvalid = new("Auth.RefreshTokenInvalid", "Refresh Token Invalid", ErrorType.Validation);
        public static Error RegistrationFailed = new("Auth.RegistrationFailed", "An Error Occurred While Registeration", ErrorType.Validation);
        public static Error AccountLocked = new("Auth.AccountLocked", "Account Locked Due To Many Failure Attempts To Login", ErrorType.Forbidden);
        public static Error AccountDeactivated = new("Auth.AccountDeactivated", "This account is deactivated.", ErrorType.Forbidden);
        public static Error InvalidRole = new("Auth.InvalidRole", "Invalid role.", ErrorType.Validation);


    }
}
