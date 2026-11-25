using DonationHub.Domain.Interfaces;

namespace DonationHub.Domain.Payments
{
    public interface IPaymentAttemptRepository : IGenericRepository<PaymentAttempt, Guid>
    {
    }
}
