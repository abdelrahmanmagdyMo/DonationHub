using DonationHub.Application.IdentityExceptions;
using Microsoft.AspNetCore.Http;

namespace DonationHub.Infrastructure.IdentityExceptions
{
    public sealed class ForbiddenAppException(string code, string msg)
        : AppException(code, msg, StatusCodes.Status403Forbidden)
    {
    }
}
