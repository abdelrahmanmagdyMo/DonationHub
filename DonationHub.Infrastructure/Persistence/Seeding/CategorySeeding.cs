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
        Category.Create(
            LocalizedText.Create("الصحة", "Health"),
            LocalizedText.Create("دعم العلاج الطبي والأدوية والخدمات الصحية",
                                 "Support for medical treatment, medicines, and healthcare services.")),
        Category.Create(
            LocalizedText.Create("التعليم", "Education"),
            LocalizedText.Create("تمويل الرسوم الدراسية والمستلزمات المدرسية وفرص التعلم",
                                 "Funding for tuition, school supplies, and learning opportunities.")),
        Category.Create(
            LocalizedText.Create("الإغاثة", "Relief"),
            LocalizedText.Create("مساعدات عاجلة للأسر المتضررة من الكوارث والفقر والأزمات",
                                 "Urgent aid for families affected by disasters, poverty, and crises.")),
        Category.Create(
            LocalizedText.Create("الأيتام", "Orphans"),
            LocalizedText.Create("رعاية وكفالة وتعليم للأطفال الأيتام",
                                 "Care, sponsorship, and education for orphaned children.")),
        Category.Create(
            LocalizedText.Create("البيئة", "Environment"),
            LocalizedText.Create("مشاريع المياه النظيفة وزراعة الأشجار وبيئة مستدامة",
                                 "Projects for clean water, tree planting, and a sustainable environment."))
    };

            await context.Categories.AddRangeAsync(categories, ct);
            await context.SaveChangesAsync(ct);
        }
    }
}

