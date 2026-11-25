using DonationHub.Domain.Campaigns;

namespace DonationHub.Application.Features.Categories.Dtos
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public ICollection<Campaign> Campaigns { get; set; }

    }
}
