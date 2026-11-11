namespace DonationHub.Infrastructure.Persistence.Identity
{

    public sealed class RefreshToken
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string TokenHash { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset ExpiresAt { get; private set; }
        public DateTimeOffset? RevokedAt { get; private set; }
        public Guid? ReplacedByTokenId { get; private set; }
        public bool IsActive(DateTimeOffset now) => RevokedAt is null && ExpiresAt > now;
        public bool IsRevoked => RevokedAt is not null;
        private RefreshToken(Guid userId, string tokenHash, DateTimeOffset createdAt, DateTimeOffset expiresAt)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            TokenHash = tokenHash;
            CreatedAt = createdAt;
            ExpiresAt = expiresAt;

        }
#pragma warning disable CS8618
        private RefreshToken() { }
        public void Revoke()
        {
            if (IsRevoked) return;
            RevokedAt = DateTimeOffset.UtcNow;
        }
        public static RefreshToken Create(Guid userId, string tokenHash, DateTimeOffset now, DateTimeOffset expiresAt)
        {
            if (userId == Guid.Empty) throw new ArgumentException("User Id Cannot Be Empty");
            ArgumentException.ThrowIfNullOrWhiteSpace(tokenHash);
            if (expiresAt <= now) throw new ArgumentException("ExpiresAt must be after CreatedAt.");
            return new RefreshToken(userId, tokenHash, now, expiresAt);
        }



    }
}
