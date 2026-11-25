using DonationHub.Domain.Common.Results;

namespace DonationHub.Application.Exceptions
{
    public sealed class UnauthorizedException(string msg) :
       AppException(msg, ErrorType.Unauthorized)
    {
    }
}
