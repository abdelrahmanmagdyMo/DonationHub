using DonationHub.Domain.Common.Results;

namespace DonationHub.Application.Exceptions
{
    public sealed class ForbiddenException(string msg) :
        AppException(msg, ErrorType.Forbidden)
    {
    }
}







