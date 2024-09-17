namespace FastMeiliSync.Application.Features.MeiliSearches.Commands.Update;

public sealed record UpdateMeiliSearchResult(
    Guid Id,
    string Label,
    string ApiKey,
    string Url,
    DateTime CreatedOn
)
{
    public static implicit operator UpdateMeiliSearchResult(MeiliSearch meiliSearch) =>
        new(
            meiliSearch.Id,
            meiliSearch.Label,
            meiliSearch.ApiKey,
            meiliSearch.Url,
            meiliSearch.CreatedOn
        );
}
