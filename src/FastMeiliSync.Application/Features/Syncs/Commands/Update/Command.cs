namespace FastMeiliSync.Application.Features.Syncs.Commands.Update;

public sealed record UpdateSyncCommand(Guid Id, string Label, Guid SourceId, Guid MeiliSearchId)
    : IRequest<Response>;
