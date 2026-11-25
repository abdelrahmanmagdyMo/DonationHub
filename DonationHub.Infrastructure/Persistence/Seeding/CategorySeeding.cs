using DonationHub.Domain.Categories;
using DonationHub.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace DonationHub.Infrastructure.Persistence.Seeding
{
    public static class CategorySeeding
    {
        public static async Task SeedAsync(AppDbContext context, CancellationToken ct = default)
        {
            if (await context.Categories.AnyAsync(ct)) return;
            var categories = new[]
    {
       CreateCategory(
            "الصحة", "Health",
            "دعم العلاج الطبي والأدوية والخدمات الصحية",
                                 "Support for medical treatment, medicines, and healthcare services."),
        CreateCategory(
           "التعليم", "Education",
            "تمويل الرسوم الدراسية والمستلزمات المدرسية وفرص التعلم",
                                 "Funding for tuition, school supplies, and learning opportunities."),
        CreateCategory("الإغاثة", "Relief",
           "مساعدات عاجلة للأسر المتضررة من الكوارث والفقر والأزمات",
                                 "Urgent aid for families affected by disasters, poverty, and crises."),
        CreateCategory(
           "الأيتام", "Orphans",
            "رعاية وكفالة وتعليم للأطفال الأيتام",
                                 "Care, sponsorship, and education for orphaned children."),
        CreateCategory(
            "البيئة", "Environment",
            "مشاريع المياه النظيفة وزراعة الأشجار وبيئة مستدامة",
                                 "Projects for clean water, tree planting, and a sustainable environment.")
    };

            await context.Categories.AddRangeAsync(categories.OfType<Category>(), ct);
            await context.SaveChangesAsync(ct);
        }
        private static Category? CreateCategory(string nameAr, string nameEn, string descriptionAr, string descriptionEn)
        {
            var name = LocalizedText.Create(nameAr, nameEn);
            var description = LocalizedText.Create(descriptionAr, descriptionEn);
            var category = Category.Create(name, description);
            return category.IsFailure ? null : category.Value;
        }
    }
}

