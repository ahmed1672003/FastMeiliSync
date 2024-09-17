namespace FastMeiliSync.Application.Features.Syncs.Commands.Create;

public sealed record CreateSyncCommand(string Label, Guid SourceId, Guid MeiliSearchId)
    : IRequest<Response>
{
    public static implicit operator Sync(CreateSyncCommand command) =>
        Sync.Create(command.Label, command.SourceId, command.MeiliSearchId);
}
