using System.ComponentModel.DataAnnotations;

namespace DonationHub.Infrastructure.Options
{
    public sealed class DonationOptions
    {
        public const string SectionName = "DonationOptions";
        [Range(0.01, double.MaxValue)]
        public decimal MinAmount { get; init; } = 1.00m;

        [Range(0.01, double.MaxValue)]
        public decimal MaxAmount { get; init; } = 10_000.00m;

        [Range(1, 1440)]
        public int PendingTimeoutMinutes { get; init; } = 30;

        [Required, StringLength(3, MinimumLength = 3)]
        public string DefaultCurrency { get; init; } = "USD";
    }
}
