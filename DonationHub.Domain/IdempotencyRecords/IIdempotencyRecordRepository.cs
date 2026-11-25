using DonationHub.Domain.Interfaces;

namespace DonationHub.Domain.IdempotencyRecords
{
    public interface IIdempotencyRecordRepository : IGenericRepository<IdempotencyRecord, string>
    {
    }
}
