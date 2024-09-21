namespace FastMeiliSync.Application.Features.MeiliSearches.Queries.Paginate;

public sealed record PaginateSyncResult(
    Guid Id,
    string Label,
    DateTime CreatedOn,
    GetMeiliSearchByIdResult MeiliSearch,
    GetSourceByIdResult Source
)
{
    public static implicit operator PaginateSyncResult(Sync sync) =>
        new(sync.Id, sync.Label, sync.CreatedOn, sync.MeiliSearch, sync.Source);
}
