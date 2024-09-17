namespace FastMeiliSync.Application.Features.MeiliSearches.Queries.GetById;

public sealed record GetMeiliSearchByIdResult(
    Guid Id,
    string Label,
    string ApiKey,
    string Url,
    DateTime CreatedOn
)
{
    public static implicit operator GetMeiliSearchByIdResult(MeiliSearch meiliSearch) =>
        new(
            meiliSearch.Id,
            meiliSearch.Label,
            meiliSearch.ApiKey,
            meiliSearch.Url,
            meiliSearch.CreatedOn
        );
}
