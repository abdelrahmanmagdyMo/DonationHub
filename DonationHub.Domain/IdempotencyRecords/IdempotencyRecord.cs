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
        public static IdempotencyRecord Create(string key, Guid userId, string requestHash, int responseStatusCode, string responseBody, DateTimeOffset createdAt, DateTimeOffset expiresAt)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(key);
            ArgumentException.ThrowIfNullOrWhiteSpace(requestHash);
            ArgumentException.ThrowIfNullOrEmpty(responseBody);
            if (expiresAt <= createdAt) throw new ArgumentException("Expire Date Must Be After Creation Date");
            key = key.Trim();
            if (key.Length > KeyMaxLength) throw new ArgumentException("Key Can not Exceed The Maximum Value");
            if (responseStatusCode is < 100 or > 599) throw new ArgumentException(nameof(responseStatusCode));
            if (userId == Guid.Empty) throw new ArgumentException("UserId Cannot Be Null Or Empty");
            return new IdempotencyRecord(key, userId, requestHash, responseStatusCode, responseBody, createdAt, expiresAt);
        }

    }
}
