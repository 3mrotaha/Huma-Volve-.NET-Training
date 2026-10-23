using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Prodcuts.Domain.Interfaces;

namespace Prodcuts.Domain.Interceptors;

public class SoftDeleteInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        foreach(var entry in eventData?.Context?.ChangeTracker?.Entries())
        {
            if(entry is not null 
                && entry.State == EntityState.Deleted 
                && entry is ISoftDeletable softDeletableItem)
            {
                entry.State = EntityState.Modified;
                softDeletableItem.SoftDelete();
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
