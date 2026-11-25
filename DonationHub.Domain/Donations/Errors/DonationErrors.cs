using DonationHub.Domain.Common.Results;

namespace DonationHub.Domain.Donations.Errors
{
    public static class DonationErrors
    {
        public static Error Validation => new("Donation.Validation", "Input validation failure", ErrorType.Validation);
        public static Error NotFound => new("Donation.NotFound", "Donation not found or not owned", ErrorType.NotFound);
        public static Error InvalidState => new("Donation.InvalidState", "Donation already in a final state", ErrorType.Conflict);
        public static Error AmountOutOfRange => new("Donation.AmountOutOfRange", "Amount outside the DonationOptions range", ErrorType.Validation);
        public static Error campaignIdIsReuired => new("Donation.campaignIdIsReuired", "campaign Id Is Reuired", ErrorType.Validation);
        public static Error DonorUserId => new("Donation.DonorUserId", "Donor Id Is Reuired", ErrorType.Validation);
        public static Error AmountIsRequired => new("Donation.AmountIsRequired", "Amount Is Required", ErrorType.Validation);
        public static Error CurrencyIsRequired => new("Donation.AmountOutOfRange", "Amount outside the DonationOptions range", ErrorType.Validation);
    }
}
