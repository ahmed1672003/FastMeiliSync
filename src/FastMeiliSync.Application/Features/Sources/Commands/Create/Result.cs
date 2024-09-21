namespace FastMeiliSync.Application.Features.Sources.Commands.Create;

public sealed record CreateSourceResult(
    Guid Id,
    string Label,
    string Database,
    string Url,
    SourceType Type,
    DateTime CreatedOn
)
{
    public static implicit operator CreateSourceResult(Source source) =>
        new(source.Id, source.Label, source.Database, source.Url, source.Type, source.CreatedOn);
}
