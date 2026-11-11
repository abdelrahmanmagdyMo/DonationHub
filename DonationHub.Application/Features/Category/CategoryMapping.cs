namespace DonationHub.Application.Features.Category
{
    public static class CategoryMapping
    {
        public static CategoryDto ToDto(this Domain.Categories.Category c)
        {
            return new CategoryDto(c.Id, c.Name.English, c.Name.Arabic, c.Description.English, c.Description.Arabic);
        }

    }
}
