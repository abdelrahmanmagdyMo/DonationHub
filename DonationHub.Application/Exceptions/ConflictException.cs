using DonationHub.Domain.Common.Results;

namespace DonationHub.Application.Exceptions
{
    public sealed class ConflictException(string msg) :
        AppException(msg, ErrorType.Conflict)
    {
    }
}
