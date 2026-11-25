

using DonationHub.Domain.Common.Results;

namespace DonationHub.Domain.ProcessedStripeEvents.Errors
{
    public static class ProcessedStripeEventErrors
    {
        public static Error EventIdIsRequired => new("ProcessedStripeEvent.EventIdIsRequired", "Event Id Is Required", ErrorType.Validation);
        public static Error TypeIsRequired => new("ProcessedStripeEvent.TypeIsRequired", "Type Is Required", ErrorType.Validation);
        public static Error EventIdMaxLength => new("ProcessedStripeEvent.EventIdMaxLength", "Event Id Cannot Exceed 255 letters", ErrorType.Validation);
        public static Error TypeMaxLength => new("ProcessedStripeEvent.TypeMaxLength", "Type Cannot Exceed 100 letters", ErrorType.Validation);

    }
}
