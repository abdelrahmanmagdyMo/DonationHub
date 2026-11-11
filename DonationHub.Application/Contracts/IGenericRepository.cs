namespace DonationHub.Application.Contracts
{
    public interface IGenericRepository<T, TKey>
        where T : class
        where TKey : notnull
    {
        Task<T?> GetByIdAsync(TKey id, CancellationToken ct = default);
        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default);
        Task AddAsync(T entity, CancellationToken ct = default);
        void Update(T entity);
    }
}
