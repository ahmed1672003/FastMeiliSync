using FastMeiliSync.Domain.Entities.Roles;

namespace FastMeiliSync.Application.Features.Roles.Commands.Create;

public sealed record CreateRoleCommand(string Name) : IRequest<Response>
{
    public static implicit operator Role(CreateRoleCommand command) => Role.Create(command.Name);
}
