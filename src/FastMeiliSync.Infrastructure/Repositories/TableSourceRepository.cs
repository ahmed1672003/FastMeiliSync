namespace FastMeiliSync.Infrastructure.Repositories;

internal sealed class TableSourceRepository : Repository<TableSource, Guid>, ITableSourceRepository
{
    public TableSourceRepository(IMeiliSyncDbContext meiliSyncDbContext)
        : base(meiliSyncDbContext) { }
}
