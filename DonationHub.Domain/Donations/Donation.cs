using DonationHub.Domain.Campaigns;
using DonationHub.Domain.Common;
using DonationHub.Domain.Donations.Enums;
using DonationHub.Domain.Exceptions;
using DonationHub.Domain.ValueObjects;

namespace DonationHub.Domain.Donations
{
    public sealed class Donation : BaseEntity
    {

        public Guid DonorUserId { get; private set; }
        public Money Amount { get; private set; } = null!;
        public DateTimeOffset CreatedAt { get; private set; }
        public DonationStatus Status { get; private set; }
        public string? FailureReason { get; private set; }
        public string? ReceiptNumber { get; private set; }
        public string? ReceiptFileName { get; private set; }
        public Guid CampaignId { get; private set; }
        public Campaign Campaign { get; private set; } = null!;
        private Donation() : base(Guid.Empty) { }
        private Donation(
Guid campaignId,
Guid donorUserId,
Money amount,
DateTimeOffset now) : base(Guid.Empty)
        {
            CampaignId = campaignId;
            DonorUserId = donorUserId;
            Amount = amount;
            Status = DonationStatus.Pending;
            CreatedAt = now;
        }
        public static Donation Create(Guid campaignId, Guid donorUserId, Money amount, DateTimeOffset now)
        {
            if (campaignId == Guid.Empty) throw new ArgumentException("campaignId Cannot Be Empty");
            if (donorUserId == Guid.Empty) throw new ArgumentException("donorUserId Cannot Be Empty");
            if (amount is null || amount.Amount <= 0) throw new Money_Amount_Exception();
            if (amount.Currency is null || string.IsNullOrWhiteSpace(amount.Currency))
                throw new Money_CurrencyException();

            return new Donation(campaignId, donorUserId, amount, now);
        }

    }
}
