namespace FastMeiliSync.Infrastructure.Repositories;

public class Repository<TEntity, TId> : IRepository<TEntity, TId>
    where TEntity : Entity<TId>
    where TId : notnull
{
    readonly IMeiliSyncDbContext _context;

    protected DbSet<TEntity> _entities;

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

    public async Task<IReadOnlyCollection<TResult>> GetAllAsync<TResult>(
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

    public Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
        bool skipQueryFilter = false,
        CancellationToken cancellationToke = default
    )
    {
        var query = _entities.AsQueryable();

        if (skipQueryFilter)
            query = query.IgnoreQueryFilters();

        if (includes != null)
            query = includes(query);

        if (filter != null)
            return query.AnyAsync(filter, cancellationToke);

        return query.AnyAsync(cancellationToke);
    }

    public async Task<IReadOnlyCollection<TResult>> PaginateAsync<TResult>(
        int pageNumber,
        int pageSize,
        Expression<Func<TEntity, object>> orderBy,
        OrderByDirection orderByDirection = OrderByDirection.Ascending,
        bool orderBeforPagination = false,
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

        if (orderBeforPagination)
        {
            if (orderByDirection == OrderByDirection.Ascending)
                query = query.OrderBy(orderBy);
            else
                query = query.OrderByDescending(orderBy);

            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
        else
        {
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            if (orderByDirection == OrderByDirection.Ascending)
                query = query.OrderBy(orderBy);
            else
                query = query.OrderByDescending(orderBy);
        }

        return await query.Select(selector).ToListAsync(cancellationToken);
    }

    public async Task<int> CountAsync(
        Expression<Func<TEntity, bool>> filter = null,
        CancellationToken cancellationToken = default
    )
    {
        var query = _entities.AsQueryable();

        if (filter != null)
            query = query.Where(filter);

        return await query.CountAsync(cancellationToken);
    }

    public IQueryable<TEntity> Query() => _entities.AsQueryable();
}
