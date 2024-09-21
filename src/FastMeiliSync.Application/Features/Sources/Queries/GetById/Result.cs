namespace FastMeiliSync.Application.Features.Sources.Queries.GetById;

public sealed record GetSourceByIdResult(
    Guid Id,
    string Label,
    string Database,
    string Url,
    SourceType Type
)
{
    public static implicit operator GetSourceByIdResult(Source source) =>
        new(source.Id, source.Label, source.Database, source.Url, source.Type);
}
