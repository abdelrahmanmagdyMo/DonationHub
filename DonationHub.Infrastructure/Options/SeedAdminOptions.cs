using System.ComponentModel.DataAnnotations;

namespace DonationHub.Infrastructure.Options
{
    public sealed class SeedAdminOptions
    {
        public const string SectionName = "SeedAdmin";
        [Required, EmailAddress]
        public string Email { get; init; } = string.Empty;

        [Required, MinLength(8)]
        public string Password { get; init; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string FullName { get; init; } = string.Empty;
    }
}
