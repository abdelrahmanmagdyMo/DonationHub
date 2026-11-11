using DonationHub.Domain.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DonationHub.Infrastructure.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.OwnsOne(x => x.Name, title =>
            {
                title.Property(e => e.Arabic).HasMaxLength(100).IsRequired();
                title.Property(e => e.English).HasMaxLength(100).IsRequired();
            });
            builder.OwnsOne(x => x.Description, desc =>
            {
                desc.Property(e => e.Arabic).HasMaxLength(255).IsRequired();
                desc.Property(e => e.English).HasMaxLength(255).IsRequired();
            });

        }
    }
}
