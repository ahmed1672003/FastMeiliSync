namespace FastMeiliSync.Application.Features.Syncs.Queries.GetById;

public sealed record GetSyncByIdResult(
    Guid Id,
    string Label,
    GetMeiliSearchByIdResult MeiliSearch,
    GetSourceByIdResult Source
)
{
    public static implicit operator GetSyncByIdResult(Sync sync) =>
        new(sync.Id, sync.Label, sync.MeiliSearch, sync.Source);
}
