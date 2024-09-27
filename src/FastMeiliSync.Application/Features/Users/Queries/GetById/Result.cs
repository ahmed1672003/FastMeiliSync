namespace FastMeiliSync.Application.Features.Roles.Queries.GetById;

public sealed record GetUserByIdResult(
    Guid Id,
    string Name,
    string Email,
    string UserName,
    DateTime CreatedOn,
    IEnumerable<GetRoleByIdResult> Roles
)
{
    public static implicit operator GetUserByIdResult(User user) =>
        new(
            user.Id,
            user.Name,
            user.UserName,
            user.Email,
            user.CreatedOn,
            user.UserRoles.Select(x => x.Role)
                .Select(x => new GetRoleByIdResult(x.Id, x.Name, x.CreatedOn))
        );
}
