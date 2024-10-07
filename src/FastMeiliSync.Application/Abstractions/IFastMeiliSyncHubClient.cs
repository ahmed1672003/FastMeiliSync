namespace FastMeiliSync.Application.Abstractions;

public interface IFastMeiliSyncHubClient
{
    Task NotifyMeiliSearchAsync(
        OperationType operationType,
        Response result,
        CancellationToken cancellationToken = default
    );
    Task NotifySourceAsync(
        OperationType operationType,
        Response result,
        CancellationToken cancellationToken = default
    );
    Task NotifySyncAsync(
        OperationType operationType,
        Response result,
        CancellationToken cancellationToken = default
    );
}
