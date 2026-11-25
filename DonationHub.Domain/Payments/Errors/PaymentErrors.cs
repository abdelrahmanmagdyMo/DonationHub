using DonationHub.Domain.Common.Results;

namespace DonationHub.Domain.Payments.Errors
{
    public static class PaymentErrors
    {
        public static Error Validation => new("Payment.Validation", "Input validation failure", ErrorType.Validation);


        public static Error InvalidSignature => new("Payment.InvalidSignature", "Stripe webhook signature invalid", ErrorType.Validation);
        public static Error DonationIdIsRequired => new("Payment.Validation", "Donation Id Is Required", ErrorType.Validation);
        public static Error stripeCheckoutSessionIdIsRequired => new("Payment.Validation", "stripe Checkout Session Id Is Required", ErrorType.Validation);
    }
}
