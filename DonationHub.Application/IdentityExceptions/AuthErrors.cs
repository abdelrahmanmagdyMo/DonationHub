namespace DonationHub.Application.IdentityExceptions
{
    public static class AuthErrors
    {
        public const string InvalidCredentials = "Auth.InvalidCredentials";
        public const string EmailAlreadyExists = "Auth.EmailAlreadyExists";
        public const string RefreshTokenInvalid = "Auth.RefreshTokenInvalid";
        public const string AccountDeactivated = "Auth.AccountDeactivated";
    }
}
