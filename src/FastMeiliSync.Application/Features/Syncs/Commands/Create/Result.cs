namespace FastMeiliSync.Application.Features.Syncs.Commands.Create;

public sealed record CreateSyncResult(
    Guid Id,
    string Label,
    DateTime CreatedOn,
    GetMeiliSearchByIdResult MeiliSearch,
    GetSourceByIdResult Source
)
{
    public static implicit operator CreateSyncResult(Sync sync) =>
        new(sync.Id, sync.Label, sync.CreatedOn, sync.MeiliSearch, sync.Source);
}
