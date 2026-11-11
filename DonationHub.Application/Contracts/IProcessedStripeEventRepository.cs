using DonationHub.Domain.ProcessedStripeEvent;

namespace DonationHub.Application.Contracts
{
    public interface IProcessedStripeEventRepository : IGenericRepository<ProcessedStripeEvent, string>
    {
    }
}
