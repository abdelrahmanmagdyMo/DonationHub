using DonationHub.Application.Contracts;
using DonationHub.Domain.Categories;
using DonationHub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DonationHub.Infrastructure.Concrete
{
    public class CategoryRepository(AppDbContext context) : IGenericRepository<Category, Guid>, ICategoryRepository
    {
        public async Task AddAsync(Category entity, CancellationToken ct = default)
        {
            await context.Categories.AddAsync(entity, ct);
        }

        public void Delete(Category entity)
        {
            context.Categories.Remove(entity);
        }

        public async Task<IReadOnlyList<Category>> GetAllAsync(CancellationToken ct = default)
        {
            return await context.Categories.ToListAsync(ct);
        }

        public async Task<Category?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await context.Categories.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public void Update(Category entity)
        {
            context.Categories.Update(entity);
        }


    }
}
