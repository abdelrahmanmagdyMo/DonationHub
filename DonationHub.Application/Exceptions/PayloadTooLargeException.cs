using DonationHub.Domain.Common.Results;

namespace DonationHub.Application.Exceptions
{
    public sealed class PayloadTooLargeException(string msg) :
        AppException(msg, ErrorType.PayloadTooLarge)
    {
    }
}







