using FastMeiliSync.Domain.Base;

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
    public Task<TEntity> GetByIdAsync(
        TId id,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
        bool stopTracking = true,
        CancellationToken cancellationToken = default
    );

    public Task<TResult?> GetAsync<TResult>(
        Expression<Func<TEntity, bool>> criteria,
        Expression<Func<TEntity, TResult>> selector = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
        bool stopTracking = true,
        CancellationToken cancellationToken = default
    );

    public Task<IReadOnlyCollection<TResult>> GetQueryAsync<TResult>(
        Expression<Func<TEntity, bool>> criteria = null,
        Expression<Func<TEntity, TResult>> selector = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
        bool stopTracking = true,
        CancellationToken cancellationToken = default
    );
}
