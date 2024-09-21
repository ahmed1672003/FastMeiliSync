namespace FastMeiliSync.Application.Features.Sources.Commands.Create;

public sealed record CreateSourceCommand(string Label, string Database, string Url, SourceType Type)
    : IRequest<Response>
{
    public static implicit operator Source(CreateSourceCommand command) =>
        Source.Create(command.Label, command.Database, command.Url, command.Type);
}
