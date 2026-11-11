namespace DonationHub.Application.Contracts
{
    public interface IDeletableRepository<T, TKey>
        where T : class
        where TKey : notnull
    {
        void Delete(T entity);
    }
}
