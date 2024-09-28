namespace FastMeiliSync.Application.Features.Sources.Queries.Paginate;

public sealed record PaginateSourceResult(
    Guid Id,
    string Label,
    string Database,
    string Url,
    DateTime CreatedOn
)
{
    public static implicit operator PaginateSourceResult(Source source) =>
        new(source.Id, source.Label, source.Database, source.Url, source.CreatedOn);
}
