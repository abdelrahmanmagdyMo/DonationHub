using DonationHub.Domain.IdempotencyRecords;

namespace DonationHub.Application.Contracts
{
    public interface IIdempotencyRecordRepository : IGenericRepository<IdempotencyRecord, string>
    {
    }
}
