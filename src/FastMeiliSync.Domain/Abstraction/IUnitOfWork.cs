using FastMeiliSync.Domain.Entities.Roles;
using FastMeiliSync.Domain.Entities.Tables;
using FastMeiliSync.Domain.Entities.TableSources;
using FastMeiliSync.Domain.Entities.Tokens;
using FastMeiliSync.Domain.Entities.Users;
using FastMeiliSync.Domain.Entities.UsersRoles;

namespace FastMeiliSync.Domain.Abstraction;

public interface IMeiliSyncUnitOfWork : IAsyncDisposable, IDisposable
{
    IMeiliSearchRepository MeiliSearches { get; }
    ISourceRepository Sources { get; }
    ISyncRepository Syncs { get; }
    ITableRepository Tables { get; }
    ITableSourceRepository TableSources { get; }
    IRoleRepository Roles { get; }
    IUserRepository Users { get; }
    IUserRoleRepository UsersRoles { get; }
    ITokenRepository Tokens { get; }

    Task<bool> SaveChangesAsync(int modifiedRows, CancellationToken cancellationToken = default);
    Task<IDbContextTransaction> BeginTransactionAsync(
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
        CancellationToken cancellationToken = default
    );
}
