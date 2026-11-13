
using DonationHub.Domain.Common.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DonationHub.Infrastructure.Interceptor
{
    public class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            if (eventData.Context is null) return result;
            foreach (var entry in eventData.Context.ChangeTracker.Entries())
            {
                if (entry is not { State: EntityState.Deleted, Entity: ISoftDeletable entity })
                    continue;

                entry.State = EntityState.Modified;
                entity.Delete();
            }
            return result;
        }
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if (eventData.Context is null) return ValueTask.FromResult(result);
            foreach (var entry in eventData.Context.ChangeTracker.Entries())
            {
                if (entry is not { State: EntityState.Deleted, Entity: ISoftDeletable entity })
                    continue;
                entry.State = EntityState.Modified;
                entity.Delete();
            }
            return ValueTask.FromResult(result);
        }
    }
}
