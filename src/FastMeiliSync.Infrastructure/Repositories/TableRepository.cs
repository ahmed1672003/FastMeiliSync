namespace FastMeiliSync.Infrastructure.Repositories;

public sealed class TableRepository : Repository<Table, Guid>, ITableRepository
{
    public TableRepository(IMeiliSyncDbContext meiliSyncDbContext)
        : base(meiliSyncDbContext) { }
}
