namespace FastMeiliSync.Infrastructure.Repositories;

public sealed class UserRepository : Repository<User, Guid>, IUserRepository
{
    public UserRepository(IMeiliSyncDbContext meiliSyncDbContext)
        : base(meiliSyncDbContext) { }

    public Task<User> GetByEmailAsync(
        string email,
        Func<IQueryable<User>, IIncludableQueryable<User, object>> includes = null,
        CancellationToken cancellationToken = default
    )
    {
        var query = _entities.AsNoTracking();
        if (includes is not null)
            query = includes(query);

        return query.FirstAsync(x => x.Email.ToLower() == email.ToLower());
    }
}
