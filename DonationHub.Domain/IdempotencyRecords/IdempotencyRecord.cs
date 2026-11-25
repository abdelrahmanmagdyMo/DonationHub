using DonationHub.Domain.Common.Results.Abstractions;
using DonationHub.Domain.IdempotencyRecords.Errors;

namespace DonationHub.Domain.IdempotencyRecords
{
    public sealed class IdempotencyRecord
    {
        public const int KeyMaxLength = 200;
        public const int RequestHashMaxLength = 64;

        public string Key { get; private set; } = null!;
        public Guid UserId { get; private set; }
        public int ResponseStatusCode { get; private set; }
        public string RequestHash { get; private set; } = null!;
        public string ResponseBody { get; private set; } = null!;
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset ExpiresAt { get; private set; }
        private IdempotencyRecord() { }
        private IdempotencyRecord(string key, Guid userId, string requestHash, int responseStatusCode, string responseBody, DateTimeOffset createdAt, DateTimeOffset expiresAt)
        {
            Key = key;
            UserId = userId;
            RequestHash = requestHash;
            ResponseStatusCode = responseStatusCode;
            ResponseBody = responseBody;
            CreatedAt = createdAt;
            ExpiresAt = expiresAt;
        }
        public static Result<IdempotencyRecord> Create(string key, Guid userId, string requestHash, int responseStatusCode, string responseBody, TimeProvider createdAt, DateTimeOffset expiresAt)
        {
            if (string.IsNullOrEmpty(key)) return Result<IdempotencyRecord>.Failure(IdempotencyErrors.KeyIsRequired);
            if (string.IsNullOrEmpty(requestHash)) return Result<IdempotencyRecord>.Failure(IdempotencyErrors.requestHashIsRequired);
            if (responseBody is null) return Result<IdempotencyRecord>.Failure(IdempotencyErrors.responseBodyIsRequired);
            if (expiresAt <= createdAt.GetUtcNow()) return Result<IdempotencyRecord>.Failure(IdempotencyErrors.EndDateInPast);
            key = key.Trim();
            var now = createdAt.GetUtcNow();

            if (key.Length > KeyMaxLength) return Result<IdempotencyRecord>.Failure(IdempotencyErrors.KeyMaxLength);
            if (responseStatusCode is < 100 or > 599) return Result<IdempotencyRecord>.Failure(IdempotencyErrors.responseStatusCodeRange);
            if (userId == Guid.Empty) return Result<IdempotencyRecord>.Failure(IdempotencyErrors.UserIdIsRequired);
            var idemPotency = new IdempotencyRecord(key, userId, requestHash, responseStatusCode, responseBody, now, expiresAt);
            return Result<IdempotencyRecord>.Success(idemPotency);
        }

    }
}
