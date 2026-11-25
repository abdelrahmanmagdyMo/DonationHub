using DonationHub.Domain.Common.Results;

namespace DonationHub.Application.Exceptions
{
    public sealed class TooManyRequestsException(string msg) :
        AppException(msg, ErrorType.TooManyRequests)
    {
    }
}







