namespace FastMeiliSync.Application.Features.MeiliSearches.Commands.Create;

public sealed record CreateMeiliSearchResult(
    Guid Id,
    string Label,
    string ApiKey,
    string Url,
    DateTime CreatedOn
)
{
    public static implicit operator CreateMeiliSearchResult(MeiliSearch meiliSearch)
    {
        return new CreateMeiliSearchResult(
            meiliSearch.Id,
            meiliSearch.Label,
            meiliSearch.ApiKey,
            meiliSearch.Url,
            meiliSearch.CreatedOn
        );
    }
}
