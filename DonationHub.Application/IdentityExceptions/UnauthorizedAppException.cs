using DonationHub.Application.IdentityExceptions;
using Microsoft.AspNetCore.Http;

namespace DonationHub.Infrastructure.IdentityExceptions
{
    public sealed class UnauthorizedAppException(string code, string msg)
        : AppException(code, msg, StatusCodes.Status401Unauthorized)
    {

    }
}
