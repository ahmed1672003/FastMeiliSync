namespace FastMeiliSync.Application.Features.Roles.Commands.Create;

public sealed record CreateRoleResult(Guid Id, string Name, DateTime CreatedOn)
{
    public static implicit operator CreateRoleResult(Role role) =>
        new(role.Id, role.Name, role.CreatedOn);
}
