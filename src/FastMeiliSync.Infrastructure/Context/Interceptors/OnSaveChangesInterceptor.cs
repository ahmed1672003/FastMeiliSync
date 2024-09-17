namespace FastMeiliSync.Infrastructure.Context.Interceptors;

public sealed class OnSaveChangesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default
    )
    {
        if (eventData.Context is null)
            return ValueTask.FromResult(result);

        foreach (var entry in eventData.Context.ChangeTracker.Entries())
        {
            if (entry is { State: EntityState.Added, Entity: ITrackableCreate createdEntity })
            {
                createdEntity.SetCreatedOn();
            }

            if (entry is { State: EntityState.Modified, Entity: ITrackableUpdate modifiedEntity })
            {
                modifiedEntity.SetUpdatedOn();
            }

            if (entry is { State: EntityState.Deleted, Entity: ITrackableDelete deletedEntity })
            {
                entry.State = EntityState.Modified;
                deletedEntity.SetDeletedOn();
            }
        }
        return ValueTask.FromResult(result);
    }
}
