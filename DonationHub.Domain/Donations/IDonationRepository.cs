using DonationHub.Domain.Interfaces;

namespace DonationHub.Domain.Donations
{
    public interface IDonationRepository : IGenericRepository<Donation, Guid>
    {
    }
}
