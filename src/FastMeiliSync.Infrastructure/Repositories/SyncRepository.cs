namespace FastMeiliSync.Infrastructure.Repositories;

public sealed class SyncRepository : Repository<Sync, Guid>, ISyncRepository
{
    public SyncRepository(IMeiliSyncDbContext meiliSyncDbContext)
        : base(meiliSyncDbContext) { }
}
