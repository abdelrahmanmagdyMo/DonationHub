using DonationHub.Domain.Common;
using DonationHub.Domain.Payments.Enums;
using DonationHub.Domain.ValueObjects;

namespace DonationHub.Domain.Payments
{
    public class PaymentAttempt : SoftDeletableEntity
    {
        public Guid DonationId { get; private set; }
        public string StripeCheckoutSessionId { get; private set; } = null!;
        public string? StripePaymentIntentId { get; private set; }
        public Money Amount { get; private set; } = null!;
        public PaymentAttemptStatus Status { get; private set; }
        public string? FailureCode { get; private set; }
        private PaymentAttempt() : base(Guid.Empty) { }
        private PaymentAttempt(Guid donationId, string stripeCheckoutSessionId, Money amount) : base(Guid.Empty)
        {
            DonationId = donationId;
            StripeCheckoutSessionId = stripeCheckoutSessionId;
            Amount = amount;
            Status = PaymentAttemptStatus.Created;
        }
        public static PaymentAttempt Create(Guid donationId, string stripeCheckoutSessionId, Money amount)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(stripeCheckoutSessionId);
            if (donationId == Guid.Empty) throw new ArgumentException("Donation Id cannot be empty.");
            return new PaymentAttempt(donationId, stripeCheckoutSessionId.Trim(), amount);
        }
    }
}
