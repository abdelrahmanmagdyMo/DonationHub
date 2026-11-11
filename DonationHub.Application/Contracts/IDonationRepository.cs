using DonationHub.Domain.Donations;

namespace DonationHub.Application.Contracts
{
    public interface IDonationRepository : IGenericRepository<Donation, Guid>
    {
    }
}
