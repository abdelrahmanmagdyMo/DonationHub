
using DonationHub.Domain.Persistence.Identity;
using Microsoft.AspNetCore.Identity;

namespace DonationHub.Infrastructure.Persistence.Identity
{
    public sealed class ApplicationUser : IdentityUser<Guid>
    {
        public string FullName { get; private set; }
        public PrefferedLanguage PreferredLanguage { get; private set; }
        public bool IsActive { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public ICollection<RefreshToken> RefreshTokens { get; private set; } = new List<RefreshToken>();
#pragma warning disable CS8618
        private ApplicationUser() { }

        private ApplicationUser(
            string fullName,
            string email,
             PrefferedLanguage prefferedLanguage,
            DateTimeOffset createdAt)
        {
            Id = Guid.NewGuid();
            FullName = fullName;
            Email = email;
            UserName = email;
            PreferredLanguage = prefferedLanguage;
            IsActive = true;
            CreatedAt = createdAt;
        }

        public static ApplicationUser Create(
            string fullName,
            string email,
            PrefferedLanguage preferredLanguage,
            DateTimeOffset createdAt)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(fullName);
            ArgumentException.ThrowIfNullOrWhiteSpace(email);

            return new ApplicationUser(
                fullName.Trim(),
                email.Trim(),
                preferredLanguage,
                createdAt);
        }
    }

}
