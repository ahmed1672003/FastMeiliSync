namespace FastMeiliSync.Application.Features.Syncs.Notifications.SyncStarted;

public sealed record SyncStartedNotification(Sync Sync) : INotification;
