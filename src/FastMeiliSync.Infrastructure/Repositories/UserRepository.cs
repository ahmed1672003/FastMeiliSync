namespace FastMeiliSync.Infrastructure.Repositories;

public sealed class UserRepository : Repository<User, Guid>, IUserRepository
{
    public UserRepository(IMeiliSyncDbContext meiliSyncDbContext)
        : base(meiliSyncDbContext) { }
}
