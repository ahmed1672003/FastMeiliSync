namespace FastMeiliSync.Domain.Abstraction;

public interface IRepository<TEntity, TId>
    where TEntity : Entity<TId>
    where TId : notnull
{
    ValueTask<EntityEntry<TEntity>> CreateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
    );
    ValueTask<EntityEntry<TEntity>> UpdateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
    );
    ValueTask<EntityEntry<TEntity>> DeleteAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
    );
    ValueTask DeleteRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default
    );
    Task<TEntity> GetByIdAsync(
        TId id,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
        bool stopTracking = true,
        CancellationToken cancellationToken = default
    );

    Task<TResult?> GetAsync<TResult>(
        Expression<Func<TEntity, bool>> criteria,
        Expression<Func<TEntity, TResult>> selector = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
        bool stopTracking = true,
        CancellationToken cancellationToken = default
    );

    Task<IReadOnlyCollection<TResult>> GetAllAsync<TResult>(
        Expression<Func<TEntity, bool>> criteria = null,
        Expression<Func<TEntity, TResult>> selector = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
        bool stopTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IReadOnlyCollection<TResult>> PaginateAsync<TResult>(
        int pageNumber,
        int pageSize,
        Expression<Func<TEntity, object>> orderBy,
        OrderByDirection orderByDirection = OrderByDirection.Ascending,
        bool orderBeforPagination = true,
        Expression<Func<TEntity, bool>> criteria = null,
        Expression<Func<TEntity, TResult>> selector = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
        bool stopTracking = true,
        CancellationToken cancellationToken = default
    );

    Task<int> CountAsync(
        Expression<Func<TEntity, bool>> filter = null,
        CancellationToken cancellationToken = default
    );
    Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
        bool skipQueryFilter = false,
        CancellationToken cancellationToke = default
    );
}
