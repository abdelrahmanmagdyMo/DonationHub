using DonationHub.Domain.Common.Results.Abstractions;
using DonationHub.Domain.Identity.Errors;

namespace DonationHub.Domain.Identity.RefreshToken;


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
    public void Revoke(DateTimeOffset revokedAt, Guid? replacedByTokenId = null)
    {
        if (IsRevoked) return;

        RevokedAt = revokedAt;
        ReplacedByTokenId = replacedByTokenId;
    }
    public static Result<RefreshToken> Create(Guid userId, string tokenHash, TimeProvider timeProvider, DateTimeOffset expiresAt)
    {
        if (userId == Guid.Empty) return Result<RefreshToken>.Failure(AuthErrors.Validation);
        if (tokenHash is null) return Result<RefreshToken>.Failure(AuthErrors.RefreshTokenInvalid);
        var now = timeProvider.GetUtcNow();
        if (expiresAt <= now) return Result<RefreshToken>.Failure(AuthErrors.Validation);
        var refreshToken = new RefreshToken(userId, tokenHash, now, expiresAt);
        return Result<RefreshToken>.Success(refreshToken);
    }



}

