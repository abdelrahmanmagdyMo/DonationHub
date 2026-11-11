using DonationHub.Domain.Payments;

namespace DonationHub.Application.Contracts
{
    public interface IPaymentAttemptRepository : IGenericRepository<PaymentAttempt, Guid>
    {
    }
}
