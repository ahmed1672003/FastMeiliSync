namespace FastMeiliSync.Infrastructure.Repositories;

public sealed class RoleRepository : Repository<Role, Guid>, IRoleRepository
{
    public RoleRepository(IMeiliSyncDbContext meiliSyncDbContext)
        : base(meiliSyncDbContext) { }
}
