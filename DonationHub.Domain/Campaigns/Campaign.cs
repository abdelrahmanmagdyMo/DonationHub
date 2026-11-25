using DonationHub.Domain.Campaigns.Enums;
using DonationHub.Domain.Campaigns.Errors;
using DonationHub.Domain.Categories;
using DonationHub.Domain.Common;
using DonationHub.Domain.Common.Results;
using DonationHub.Domain.Common.Results.Abstractions;
using DonationHub.Domain.Donations;
using DonationHub.Domain.ValueObjects;
namespace DonationHub.Domain.Campaigns
{
    public sealed class Campaign : SoftDeletableEntity
    {
        public LocalizedText Title { get; private set; } = null!;
        public LocalizedText Description { get; private set; } = null!;
        public Money GoalAmount { get; private set; } = null!;
        public Money RaisedAmount { get; private set; } = null!;
        public Guid CategoryId { get; private set; }
        public Guid CreatedByUserId { get; private set; }
        public DateTimeOffset EndDate { get; private set; }
        public string? BannerFileName { get; private set; }
        public CampaignStatus Status { get; private set; }

        public Category Category { get; private set; } = null!;
        public ICollection<Donation> Donations { get; private set; } = new List<Donation>();
        public byte[] RowVersion { get; private set; } = null!;

        private Campaign() : base(Guid.Empty) { }
        private Campaign(LocalizedText title, LocalizedText description,
            Money goal, Guid categoryId, DateTimeOffset endDate, Guid createdByUserId) : base(Guid.NewGuid())
        {
            Title = title;
            Status = CampaignStatus.Active;
            Description = description;
            GoalAmount = goal;
            RaisedAmount = Money.Zero(goal.Currency);
            CategoryId = categoryId;
            EndDate = endDate;
            CreatedByUserId = createdByUserId;
        }
        public static Result<Campaign> Create(LocalizedText title, LocalizedText description,
            Money goal, Guid categoryId, DateTimeOffset endDate, TimeProvider timeProvider, Guid createdByUserId)
        {
            if (title is null) return Result<Campaign>.Failure(CampaignErrors.Validation);
            if (description is null) return Result<Campaign>.Failure(CampaignErrors.Validation);
            if (goal is null || goal.Amount <= 0) return Result<Campaign>.Failure(CampaignErrors.InvalidGoal);

            if (endDate <= timeProvider.GetUtcNow()) return Result<Campaign>.Failure(CampaignErrors.EndDateInPast);
            var campaign = new Campaign(title, description, goal, categoryId, endDate, createdByUserId);
            return Result<Campaign>.Success(campaign);
        }
        public Result Update(LocalizedText title, LocalizedText description, Money goal, Guid categoryId, DateTimeOffset endDate, TimeProvider timeProvider, byte[] incomingRowVersion)
        {
            if (Status != CampaignStatus.Active) return Result.Failure(CampaignErrors.NotActive);
            if (categoryId == Guid.Empty) return Result.Failure(CampaignErrors.Validation);

            if (title is null) return Result.Failure(CampaignErrors.Validation);
            if (description is null) return Result.Failure(CampaignErrors.Validation);

            if (goal is null || goal.Amount <= 0) return Result.Failure(CampaignErrors.InvalidGoal);

            if (endDate <= timeProvider.GetUtcNow()) return Result.Failure(CampaignErrors.EndDateInPast);
            if (goal.Currency != RaisedAmount.Currency || goal.Amount < RaisedAmount.Amount)
                return Result.Failure(CampaignErrors.GoalBelowRaised);
            if (RowVersion is null || !RowVersion.SequenceEqual(incomingRowVersion))
                return Result.Failure(CampaignErrors.ConcurrencyConflict);
            CategoryId = categoryId;
            Title = title;
            Description = description;
            GoalAmount = goal;
            EndDate = endDate;
            RowVersion = incomingRowVersion;
            return Result.Success();
        }
    }
}
