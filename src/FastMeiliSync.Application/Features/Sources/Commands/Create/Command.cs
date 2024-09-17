namespace FastMeiliSync.Application.Features.Sources.Commands.Create;

public sealed record CreateSourceCommand(
    string Label,
    string User,
    string Host,
    int Port,
    string Database,
    string Url,
    SourceType Type,
    IDictionary<string, string> Configurations
) : IRequest<Response>
{
    public static implicit operator Source(CreateSourceCommand command)
    {
        Source source = Source.Create(
            command.Label,
            command.User,
            command.Host,
            command.Port,
            command.Database,
            command.Url,
            command.Type
        );

        if (command.Configurations is not null && command.Configurations.Any())
            source.AddConfigurations(command.Configurations);

        return source;
    }
}
