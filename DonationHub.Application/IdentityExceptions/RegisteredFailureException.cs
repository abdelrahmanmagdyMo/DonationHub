using Microsoft.AspNetCore.Http;

namespace DonationHub.Application.IdentityExceptions
{
    public sealed class RegistrationFailedException(string code, string msg)
        : AppException(code, msg, StatusCodes.Status403Forbidden)

    {
    }
}
