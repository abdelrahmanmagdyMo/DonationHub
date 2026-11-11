using System.ComponentModel.DataAnnotations;

namespace DonationHub.Infrastructure.Options
{
    public sealed class JwtOptions
    {
        public const string SectionName = "Jwt";

        [Required, MinLength(32)]
        public string SecretKey { get; init; } = string.Empty;

        [Required]
        public string Issuer { get; init; } = string.Empty;

        [Required]
        public string Audience { get; init; } = string.Empty;

        [Range(1, 1440)]
        public int ExpiryMinutes { get; init; } = 60;

        [Range(1, 365)]
        public int RefreshTokenExpiryDays { get; init; } = 7;
    }
}
