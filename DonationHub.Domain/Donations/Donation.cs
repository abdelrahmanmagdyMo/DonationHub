using DonationHub.Domain.Campaigns;
using DonationHub.Domain.Common;
using DonationHub.Domain.Common.Results.Abstractions;
using DonationHub.Domain.Donations.Enums;
using DonationHub.Domain.Donations.Errors;
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
TimeProvider timeProvider) : base(Guid.Empty)
        {
            CampaignId = campaignId;
            DonorUserId = donorUserId;
            Amount = amount;
            Status = DonationStatus.Pending;
            CreatedAt = timeProvider.GetUtcNow();
        }
        public static Result<Donation> Create(Guid campaignId, Guid donorUserId, Money amount, TimeProvider timeProvider)
        {
            if (campaignId == Guid.Empty) return Result<Donation>.Failure(DonationErrors.campaignIdIsReuired);
            if (donorUserId == Guid.Empty) return Result<Donation>.Failure(DonationErrors.DonorUserId);
            if (amount is null || amount.Amount <= 0) return Result<Donation>.Failure(DonationErrors.AmountIsRequired);

            if (amount.Currency is null || string.IsNullOrWhiteSpace(amount.Currency))
                return Result<Donation>.Failure(DonationErrors.CurrencyIsRequired);


            var donation = new Donation(campaignId, donorUserId, amount, timeProvider);
            return Result<Donation>.Success(donation);
        }

    }
}
