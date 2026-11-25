using DonationHub.Domain.Interfaces;

namespace DonationHub.Domain.ProcessedStripeEvents
{
    public interface IProcessedStripeEventRepository : IGenericRepository<ProcessedStripeEvent, string>
    {
    }
}
