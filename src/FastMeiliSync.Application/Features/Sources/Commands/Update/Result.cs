namespace FastMeiliSync.Application.Features.Sources.Commands.Update;

public sealed record UpdateSourceResult(
    Guid Id,
    string Label,
    string Database,
    string Url,
    SourceType Type,
    DateTime CreatedOn
)
{
    public static implicit operator UpdateSourceResult(Source source) =>
        new(source.Id, source.Label, source.Database, source.Url, source.Type, source.CreatedOn);
}
