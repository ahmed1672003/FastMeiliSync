namespace FastMeiliSync.Application.Features.Sources.Commands.Update;

public sealed record UpdateSourceCommand(
    Guid Id,
    string Label,
    string User,
    string Host,
    int Port,
    string Database,
    string Url,
    SourceType Type,
    IDictionary<string, string> Configurations
) : IRequest<Response>;
