using DonationHub.Domain.Common.Results;

namespace DonationHub.Application.Exceptions
{
    public abstract class AppException(string msg, ErrorType errorType = ErrorType.Failure)
        : Exception(msg)
    {

    }
}
