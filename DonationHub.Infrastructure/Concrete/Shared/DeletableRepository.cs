using DonationHub.Domain.Interfaces;
using DonationHub.Infrastructure.Persistence;

namespace DonationHub.Infrastructure.Concrete.Shared
{
    public class DeletableRepository<T, TKey>(AppDbContext context) : IDeletableRepository<T, TKey>
        where T : class
        where TKey : notnull
    {
        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }
    }
}
