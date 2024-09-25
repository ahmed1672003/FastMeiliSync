using FastMeiliSync.Application.Features.Roles.Queries.GetById;
using FastMeiliSync.Domain.Entities.Users;

namespace FastMeiliSync.Application.Features.Users.Commands.Create;

public sealed record CreateUserResult(
    Guid Id,
    string Name,
    string UserName,
    string Email,
    DateTime CreatedOn
)
{
    public static List<GetRoleByIdResult> Roles { get; set; } = new();

    public static implicit operator CreateUserResult(User user)
    {
        var result = new CreateUserResult(
            user.Id,
            user.Name,
            user.UserName,
            user.Email,
            user.CreatedOn
        );

        if (user.UserRoles.Any() && user.UserRoles.Select(x => x.Role).Any())
        {
            foreach (var userRole in user.UserRoles)
                Roles.Add(userRole.Role);
        }
        return result;
    }
}
