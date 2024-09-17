using FastMeiliSync.Domain.Entities.Tables;
using FastMeiliSync.Domain.Entities.TableSources;

namespace FastMeiliSync.Domain.Abstraction;

public interface IMeiliSyncUnitOfWork : IAsyncDisposable, IDisposable
{
    IMeiliSearchRepository MeiliSearches { get; }
    ISourceRepository Sources { get; }
    ISyncRepository Syncs { get; }
    ITableRepository Tables { get; }
    ITableSourceRepository TableSources { get; }

    Task<bool> SaveChangesAsync(int modifiedRows, CancellationToken cancellationToken = default);
    Task<IDbContextTransaction> BeginTransactionAsync(
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
        CancellationToken cancellationToken = default
    );
}
