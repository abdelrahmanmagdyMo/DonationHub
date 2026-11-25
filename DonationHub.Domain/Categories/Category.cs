using DonationHub.Domain.Campaigns;
using DonationHub.Domain.Categories.Errors;
using DonationHub.Domain.Common;
using DonationHub.Domain.Common.Results;
using DonationHub.Domain.Common.Results.Abstractions;
using DonationHub.Domain.ValueObjects;

namespace DonationHub.Domain.Categories;

public sealed class Category : AuditableEntity
{
    public LocalizedText Name { get; private set; } = null!;
    public LocalizedText Description { get; private set; } = null!;
    public ICollection<Campaign> Campaigns { get; private set; } = new List<Campaign>();

    private Category() : base(Guid.Empty) { }

    private Category(LocalizedText name, LocalizedText description) : base(Guid.Empty)
    {
        Name = name;
        Description = description;
    }
    public static Result<Category> Create(LocalizedText name, LocalizedText description)
    {
        if (name is null) return Result<Category>.Failure(CategoryErrors.Validation);
        if (description is null) return Result<Category>.Failure(CategoryErrors.Validation);
        var category = new Category(name, description);
        return Result<Category>.Success(category);
    }
    public Result Update(LocalizedText name, LocalizedText description)
    {
        if (name is null)
            return Result.Failure(CategoryErrors.Validation);

        if (description is null)
            return Result.Failure(CategoryErrors.Validation);

        Name = name;
        Description = description;

        return Result.Success();
    }
}
