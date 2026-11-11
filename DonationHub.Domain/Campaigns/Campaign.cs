using DonationHub.Domain.Campaigns.Enums;
using DonationHub.Domain.Categories;
using DonationHub.Domain.Common;
using DonationHub.Domain.Donations;
using DonationHub.Domain.Exceptions;
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
        public static Campaign Create(LocalizedText title, LocalizedText description,
            Money goal, Guid categoryId, DateTimeOffset endDate, DateTimeOffset now, Guid createdByUserId)
        {
            EnsureGoalAmountIsPositive(goal);
            EnsureEndDateIsInFuture(endDate, now);
            return new Campaign(title, description, goal, categoryId, endDate, createdByUserId);
        }
        private static void EnsureEndDateIsInFuture(DateTimeOffset endDate, DateTimeOffset now)
        {
            if (endDate <= now) throw new EndDateInPastException();
        }
        private static void EnsureGoalAmountIsPositive(Money goal)
        {
            if (goal is null || goal.Amount <= 0) throw new GoalAmountInvalidException();
        }
    }
}
