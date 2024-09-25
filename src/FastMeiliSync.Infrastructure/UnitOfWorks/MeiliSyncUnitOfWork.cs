using FastMeiliSync.Domain.Entities.Tokens;

namespace FastMeiliSync.Infrastructure.UnitOfWorks;

public class MeiliSyncUnitOfWork : IMeiliSyncUnitOfWork
{
    readonly IMeiliSyncDbContext _context;

    public MeiliSyncUnitOfWork(
        IMeiliSyncDbContext context,
        IMeiliSearchRepository meiliSearches,
        ISourceRepository sources,
        ISyncRepository syncs,
        ITableRepository tables,
        ITableSourceRepository tableSources,
        IRoleRepository roles,
        IUserRepository users,
        IUserRoleRepository usersRoles,
        ITokenRepository tokens
    )
    {
        _context = context;
        MeiliSearches = meiliSearches;
        Sources = sources;
        Syncs = syncs;
        Tables = tables;
        TableSources = tableSources;
        Roles = roles;
        Users = users;
        UsersRoles = usersRoles;
        Tokens = tokens;
    }

    public IMeiliSearchRepository MeiliSearches { get; }
    public ISourceRepository Sources { get; }
    public ISyncRepository Syncs { get; }
    public ITableRepository Tables { get; }
    public ITableSourceRepository TableSources { get; }
    public IRoleRepository Roles { get; }
    public IUserRepository Users { get; }
    public IUserRoleRepository UsersRoles { get; }
    public ITokenRepository Tokens { get; }

    public Task<bool> SaveChangesAsync(
        int modifiedRows,
        CancellationToken cancellationToken = default
    ) => _context.SaveChangesAsync(modifiedRows, cancellationToken);

    public Task<IDbContextTransaction> BeginTransactionAsync(
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
        CancellationToken cancellationToken = default
    ) => _context.BeginTransactionAsync(isolationLevel, cancellationToken);

    public async ValueTask DisposeAsync() => await _context.DisposeAsync();

    public void Dispose() => _context.Dispose();
}
