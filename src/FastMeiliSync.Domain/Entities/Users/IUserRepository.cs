namespace FastMeiliSync.Domain.Entities.Users;

public interface IUserRepository : IRepository<User, Guid>
{
    Task<User> GetByEmailAsync(
        string email,
        Func<IQueryable<User>, IIncludableQueryable<User, object>> includes = null,
        CancellationToken cancellationToken = default
    );
}
