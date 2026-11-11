using DonationHub.Application.Contracts;
using DonationHub.Domain.Payments;
using DonationHub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DonationHub.Infrastructure.Concrete
{
    public class PaymentAttemptRepository(AppDbContext context) :
        IGenericRepository<PaymentAttempt, Guid>, IPaymentAttemptRepository
    {
        public async Task AddAsync(PaymentAttempt entity, CancellationToken ct = default)
        {
            await context.PaymentAttempts.AddAsync(entity, ct);

        }

        public async Task<IReadOnlyList<PaymentAttempt>> GetAllAsync(CancellationToken ct = default)
        {
            return await context.PaymentAttempts.ToListAsync(ct);
        }

        public async Task<PaymentAttempt?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await context.PaymentAttempts.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public void Update(PaymentAttempt entity)
        {
            context.PaymentAttempts.Update(entity);
        }
    }
}
