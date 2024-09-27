using FastMeiliSync.Application.Features.Roles.Queries.GetById;

namespace FastMeiliSync.Application.Features.Users.Commands.Create;

public sealed record CreateUserResult(
    Guid Id,
    string Name,
    string UserName,
    string Email,
    DateTime CreatedOn,
    IEnumerable<GetRoleByIdResult> Roles
)
{
    public static implicit operator CreateUserResult(User user) =>
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
