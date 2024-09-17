namespace FastMeiliSync.Infrastructure.Repositories;

public class Repository<TEntity, TId> : IRepository<TEntity, TId>
    where TEntity : Entity<TId>
    where TId : notnull
{
    readonly IMeiliSyncDbContext _context;

    readonly DbSet<TEntity> _entities;

    public Repository(IMeiliSyncDbContext meiliSyncDbContext)
    {
        _context = meiliSyncDbContext;
        _entities = _context.Set<TEntity>();
    }

    public ValueTask<EntityEntry<TEntity>> CreateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
    ) => _entities.AddAsync(entity, cancellationToken);

    public ValueTask<EntityEntry<TEntity>> UpdateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
    ) => ValueTask.FromResult(_entities.Update(entity));

    public ValueTask<EntityEntry<TEntity>> DeleteAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
    ) => ValueTask.FromResult(_entities.Remove(entity));

    public Task<TEntity> GetByIdAsync(
        TId id,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
        bool stopTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        var query = _entities.AsQueryable();
        if (stopTracking)
        {
            query = query.AsNoTracking();
        }

        if (includes != null)
            query = includes(query);

        query = query.Where(x => x.Id.Equals(id));

        return query.FirstAsync(cancellationToken);
    }

    public Task<TResult?> GetAsync<TResult>(
        Expression<Func<TEntity, bool>> criteria,
        Expression<Func<TEntity, TResult>> selector = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
        bool stopTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        var query = _entities.AsQueryable();
        if (stopTracking)
        {
            query = query.AsNoTrackingWithIdentityResolution();
        }

        if (includes != null)
            query = includes(query);

        query = query.Where(criteria);
        var result = query.Select(selector);
        return result.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<TResult>> GetQueryAsync<TResult>(
        Expression<Func<TEntity, bool>> criteria = null,
        Expression<Func<TEntity, TResult>> selector = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
        bool stopTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        var query = _entities.AsQueryable();
        if (stopTracking)
        {
            query = query.AsNoTrackingWithIdentityResolution();
        }

        if (includes != null)
            query = includes(query);

        if (criteria != null)
            query = query.Where(criteria);

        return await query.Select(selector).ToListAsync(cancellationToken);
    }
}
