using DonationHub.Domain.Common.Results;

namespace DonationHub.Domain.Categories.Errors
{
    public static class CategoryErrors
    {

        public static Error Validation => new("Category.Validation", "Input validation failure", ErrorType.Validation);
        public static Error NotFound => new("Category.NotFound", "Category does not exist", ErrorType.NotFound);
        public static Error HasCampaigns => new("Category.HasCampaigns", "Cannot delete a category with linked campaigns", ErrorType.Conflict);
    }
}
