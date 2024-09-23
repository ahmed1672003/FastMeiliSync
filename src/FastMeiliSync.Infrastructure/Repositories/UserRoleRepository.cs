using FastMeiliSync.Domain.Entities.UsersRoles;

namespace FastMeiliSync.Infrastructure.Repositories;

public sealed class UserRoleRepository : Repository<UserRole, Guid>, IUserRoleRepository
{
    public UserRoleRepository(IMeiliSyncDbContext meiliSyncDbContext)
        : base(meiliSyncDbContext) { }
}
