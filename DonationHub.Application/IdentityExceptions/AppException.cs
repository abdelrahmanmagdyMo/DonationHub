namespace DonationHub.Application.IdentityExceptions
{
    public abstract class AppException(string code, string msg, int statusCode) : Exception(msg)
    {
        public string Code { get; } = code;
        public int StatusCode { get; } = statusCode;
    }
}

