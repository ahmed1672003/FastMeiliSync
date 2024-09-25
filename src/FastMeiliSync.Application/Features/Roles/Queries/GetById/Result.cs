namespace FastMeiliSync.Application.Features.Roles.Queries.GetById;

public sealed record GetRoleByIdResult(Guid Id, string Name, DateTime CreatedOn)
{
    public static implicit operator GetRoleByIdResult(Role role) =>
        new(role.Id, role.Name, role.CreatedOn);
}
