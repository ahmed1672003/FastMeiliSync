namespace FastMeiliSync.Application.Abstractions;

public interface IWal2JosnService
{
    Task StartReplicationConnectionAsync(
        string database,
        string databaseUrl,
        string meiliSearchUrl,
        CancellationToken cancellationToken = default
    );
}
