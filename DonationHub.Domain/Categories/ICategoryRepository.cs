using DonationHub.Domain.Interfaces;
namespace DonationHub.Domain.Categories;

public interface ICategoryRepository : IGenericRepository<Category, Guid>,
   IDeletableRepository<Category, Guid>
{
}
