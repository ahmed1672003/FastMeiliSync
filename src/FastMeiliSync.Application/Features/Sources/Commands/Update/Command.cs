namespace FastMeiliSync.Application.Features.Sources.Commands.Update;

public sealed record UpdateSourceCommand(
    Guid Id,
    string Label,
    string Database,
    string Url,
    SourceType Type
) : IRequest<Response>;
