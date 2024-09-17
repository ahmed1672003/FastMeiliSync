namespace FastMeiliSync.Application.Features.Syncs.Commands.Update;

public sealed record UpdateSyncResult(
    Guid Id,
    string Label,
    DateTime CreatedOn,
    GetMeiliSearchByIdResult MeiliSearch,
    GetSourceByIdResult Source
)
{
    public static implicit operator UpdateSyncResult(Sync sync) =>
        new(sync.Id, sync.Label, sync.CreatedOn, sync.MeiliSearch, sync.Source);
}
