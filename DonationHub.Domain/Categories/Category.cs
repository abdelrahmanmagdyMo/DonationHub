using DonationHub.Domain.Campaigns;
using DonationHub.Domain.Common;
using DonationHub.Domain.ValueObjects;

namespace DonationHub.Domain.Categories
{
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
        public static Category Create(LocalizedText name, LocalizedText description)
        {
            return new Category(name, description);
        }
        public void Update(LocalizedText name, LocalizedText description)
        {
            ArgumentNullException.ThrowIfNull(name);
            ArgumentNullException.ThrowIfNull(description);
            Name = name;
            Description = description;
        }
    }
}
