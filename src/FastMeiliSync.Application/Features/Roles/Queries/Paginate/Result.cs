namespace FastMeiliSync.Application.Features.Roles.Queries.Paginate;

public sealed record PaginateRoleResult(Guid Id, string Name, DateTime CreatedOn)
{
    public static implicit operator PaginateRoleResult(Role role) =>
        new(role.Id, role.Name, role.CreatedOn);
}
