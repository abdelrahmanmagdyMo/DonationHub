using DonationHub.Domain.Common.Results;

namespace DonationHub.Domain.IdempotencyRecords.Errors
{
    public static class IdempotencyErrors
    {
        public static Error Validation => new("Idempotency.Validation", "Input validation failure", ErrorType.Validation);
        public static Error KeyIsRequired => new("Idempotency.KeyIsRequired", "Key Is Required", ErrorType.Validation);
        public static Error UserIdIsRequired => new("Idempotency.UserIdIsRequired", "User Id Is Required", ErrorType.Validation);
        public static Error requestHashIsRequired => new("Idempotency.requestHashIsRequired", "request Hash Is Required", ErrorType.Validation);
        public static Error responseBodyIsRequired => new("Idempotency.responseBodyIsRequired", "response Body Is Required", ErrorType.Validation);
        public static Error EndDateInPast => new("Idempotency.EndDateInPast", "End date must be in the future", ErrorType.Validation);
        public static Error KeyMaxLength => new("Idempotency.KeyMaxLength", "Key Cannot Exceed 200 letters ", ErrorType.Validation);
        public static Error responseStatusCodeRange => new("Idempotency.responseStatusCodeRange", "response Status Code Must Be In Range Of [100,599]", ErrorType.Validation);
        public static Error RequestHashMaxLength => new("Idempotency.RequestHashMaxLength", "Key Cannot Exceed 64 letters ", ErrorType.Validation);
        public static Error KeyReusedWithDifferentRequest => new("KeyReusedWithDifferentRequest", "Same Idempotency-Key with a different body", ErrorType.Conflict);
    }
}
