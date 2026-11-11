using DonationHub.Application.Contracts;
using DonationHub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DonationHub.Infrastructure.Concrete
{
    public class GenericRepository<T, TKey>(AppDbContext dbContext) : IGenericRepository<T, TKey> where T : class where TKey : notnull
    {
        private readonly DbSet<T> _set = dbContext.Set<T>();

        public async Task AddAsync(T entity, CancellationToken ct = default)
        {
            await _set.AddAsync(entity, ct);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default)
        {
            return await _set.ToListAsync(ct);
        }

        public async Task<T?> GetByIdAsync(TKey id, CancellationToken ct = default)
        {
            return await _set.FindAsync([id], ct);
        }

        public void Update(T entity)
        {
            _set.Update(entity);
        }

    }
}
