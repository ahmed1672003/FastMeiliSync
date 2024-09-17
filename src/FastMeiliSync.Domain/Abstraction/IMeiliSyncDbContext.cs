namespace FastMeiliSync.Domain.Abstraction;

public interface IMeiliSyncDbContext : IAsyncDisposable, IDisposable
{
    DbSet<TEntity> Set<TEntity>()
        where TEntity : class;
    Task<bool> SaveChangesAsync(int modifiedRows, CancellationToken cancellationToken = default);
    Task<IDbContextTransaction> BeginTransactionAsync(
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
        CancellationToken cancellationToken = default
    );
}
