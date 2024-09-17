namespace FastMeiliSync.Infrastructure.Repositories;

public sealed class MeiliSearchRepository : Repository<MeiliSearch, Guid>, IMeiliSearchRepository
{
    public MeiliSearchRepository(IMeiliSyncDbContext meiliSyncDbContext)
        : base(meiliSyncDbContext) { }
}
