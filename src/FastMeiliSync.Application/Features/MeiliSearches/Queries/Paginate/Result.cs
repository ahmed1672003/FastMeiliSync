namespace FastMeiliSync.Application.Features.MeiliSearches.Queries.Paginate;

public sealed record PaginateMeiliSearchResult(
    Guid Id,
    string Label,
    string ApiKey,
    string Url,
    DateTime CreatedOn
);
