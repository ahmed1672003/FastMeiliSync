using FastMeiliSync.Application.Abstractions;

namespace FastMeiliSync.Application.Features.Syncs.Notifications.SyncStarted;

public sealed class SyncStartedNotificationHandler(IWal2JosnService wal2JosnService)
    : INotificationHandler<SyncStartedNotification>
{
    public async Task Handle(
        SyncStartedNotification notification,
        CancellationToken cancellationToken
    )
    {
        await wal2JosnService.StartReplicationConnectionAsync(
            notification.Sync.Source.Database,
            notification.Sync.Source.Url,
            notification.Sync.MeiliSearch.Url,
            cancellationToken
        );
    }
}
