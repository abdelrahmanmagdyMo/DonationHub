using DonationHub.Domain.Common.Results;

namespace DonationHub.Domain.Campaigns.Errors
{
    public static class CampaignErrors
    {
        public static Error Validation => new("Campaign.Validation", "Input validation failure", ErrorType.Validation);
        public static Error NotActive => new("Campaign.NotActive", "Campaign is completed or expired", ErrorType.Conflict);
        public static Error NotFound => new("Campaign.NotFound", "Campaign ID Not Found", ErrorType.NotFound);
        public static Error GoalBelowRaised => new("Campaign.GoalBelowRaised", "Goal cannot be lower than the raised amount", ErrorType.Validation);
        public static Error EndDateInPast => new("Campaign.EndDateInPast", "End date must be in the future", ErrorType.Validation);
        public static Error ConcurrencyConflict => new("Campaign.ConcurrencyConflict", "Concurrent update detected", ErrorType.Conflict);
        public static Error CampaignHasDonations => new("Campaign.CampaignHasDonations", "Campaign Has Donations , Cannot Be Deleted", ErrorType.Conflict);
        public static Error InvalidGoal => new("Campaign.InvalidGoal", "Invalid Goal", ErrorType.Validation);
    }
}
