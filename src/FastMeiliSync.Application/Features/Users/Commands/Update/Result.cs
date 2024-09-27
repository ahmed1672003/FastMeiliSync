namespace FastMeiliSync.Application.Features.Users.Commands.Update;

public sealed record UpdateUserResult(
    Guid Id,
    string Name,
    string Email,
    string UserName,
    DateTime CreatedOn,
    IEnumerable<GetRoleByIdResult> Roles
)
{
    public static implicit operator UpdateUserResult(User user) =>
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
