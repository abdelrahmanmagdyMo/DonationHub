using DonationHub.Domain.Categories;

namespace DonationHub.Application.Contracts
{
    public interface ICategoryRepository : IGenericRepository<Category, Guid>,
        IDeletableRepository<Category, Guid>
    {
    }
}
