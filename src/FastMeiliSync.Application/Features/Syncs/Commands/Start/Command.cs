namespace FastMeiliSync.Application.Features.Syncs.Commands.Start;

public sealed record StartSyncCommand(Guid Id) : IRequest<Response>;
