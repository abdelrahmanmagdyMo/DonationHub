using DonationHub.Domain.Common.Results;

namespace DonationHub.Application.Exceptions
{
    public sealed class FailureException(string msg) :
        AppException(msg, ErrorType.Failure)
    {
    }
}







