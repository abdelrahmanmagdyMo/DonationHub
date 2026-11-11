using System.ComponentModel.DataAnnotations;

namespace DonationHub.Infrastructure.Options
{
    public class SeedDonorOptions
    {
        public const string SectionName = "SeedUsers:Donor";

        [Required, EmailAddress]
        public string Email { get; init; } = string.Empty;

        [Required, MinLength(8)]
        public string Password { get; init; } = string.Empty;

        [Required, MaxLength(100)]
        public string FullName { get; init; } = string.Empty;
    }
}
