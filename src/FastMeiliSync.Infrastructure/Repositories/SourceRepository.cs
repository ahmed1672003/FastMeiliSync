namespace FastMeiliSync.Infrastructure.Repositories;

public sealed class SourceRepository : Repository<Source, Guid>, ISourceRepository
{
    public SourceRepository(IMeiliSyncDbContext meiliSyncDbContext)
        : base(meiliSyncDbContext) { }
}
