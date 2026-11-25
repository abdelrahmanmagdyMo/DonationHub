using DonationHub.Domain.Common.Results;

namespace DonationHub.Application.Exceptions
{
    public sealed class BadRequestException(string msg) :
        AppException(msg, ErrorType.Validation)
    {
    }
}







