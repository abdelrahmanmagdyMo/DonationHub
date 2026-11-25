using DonationHub.Domain.Common.Results;

namespace DonationHub.Application.Exceptions
{
    public sealed class ValidationException(IDictionary<Error, string[]> Errors)
        : AppException($"One Or More Errors Occurred {string.Join("; ", Errors.SelectMany(c => c.Value))}"
            , ErrorType.Validation)
    {
    }
}
