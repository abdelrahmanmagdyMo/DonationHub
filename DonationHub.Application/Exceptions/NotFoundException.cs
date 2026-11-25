using DonationHub.Domain.Common.Results;

namespace DonationHub.Application.Exceptions
{
    public class NotFoundException(string resourceName, object key)
        : AppException($"{resourceName} with identifier {key} was not found", ErrorType.NotFound)
    {
    }
}
