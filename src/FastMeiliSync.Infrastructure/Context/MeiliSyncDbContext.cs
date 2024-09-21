namespace FastMeiliSync.Infrastructure.Context;

public sealed class MeiliSyncDbContext : DbContext, IMeiliSyncDbContext
{
    public MeiliSyncDbContext(DbContextOptions<MeiliSyncDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public async Task<bool> SaveChangesAsync(
        int modifiedRows,
        CancellationToken cancellationToken = default
    ) => await SaveChangesAsync(cancellationToken) == modifiedRows;

    public DbSet<TEntity> Set<TEntity>()
        where TEntity : class => base.Set<TEntity>();

    public Task<IDbContextTransaction> BeginTransactionAsync(
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
        CancellationToken cancellationToken = default
    ) => Database.BeginTransactionAsync(isolationLevel, cancellationToken);
}
