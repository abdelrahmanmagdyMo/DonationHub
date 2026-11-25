using DonationHub.Application.Features.Categories.Dtos;
using domainCategory = DonationHub.Domain.Categories;

namespace DonationHub.Application.Features.Categories.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryDto ToDto(this domainCategory.Category category)
        {
            ArgumentNullException.ThrowIfNull(category);
            return new CategoryDto
            {
                Id = category.Id,
                NameAr = category.Name.Arabic,
                NameEn = category.Name.English,
                DescriptionAr = category.Description.Arabic,
                DescriptionEn = category.Description.English,
                Campaigns = category.Campaigns
            };
        }
        public static IEnumerable<CategoryDto> ToDtos(this IEnumerable<domainCategory.Category> categories)
        {
            return [.. categories.Select(c => c.ToDto())];
        }
    }
}
