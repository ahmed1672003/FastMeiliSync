namespace FastMeiliSync.Application.Features.Syncs.Commands.Stop;

public sealed record StopSyncCommand(Guid Id) : IRequest<Response>;
