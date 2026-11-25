using DonationHub.Domain.Common.Results;

namespace DonationHub.Application.Exceptions
{
    public sealed class UnprocessableEntityException(string msg) :
        AppException(msg, ErrorType.UnprocessableEntity)
    {
    }
}







