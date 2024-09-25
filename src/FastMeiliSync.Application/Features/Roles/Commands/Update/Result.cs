namespace FastMeiliSync.Application.Features.Roles.Commands.Update;

public sealed record UpdateRoleResult(Guid Id, string Name, DateTime CreatedOn)
{
    public static implicit operator UpdateRoleResult(Role role) =>
        new(role.Id, role.Name, role.CreatedOn);
}
