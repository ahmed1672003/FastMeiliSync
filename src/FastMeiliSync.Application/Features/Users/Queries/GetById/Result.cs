using FastMeiliSync.Domain.Entities.Users;

namespace FastMeiliSync.Application.Features.Roles.Queries.GetById;

public sealed record GetUserByIdResult(
    Guid Id,
    string Name,
    string Email,
    string UserName,
    DateTime CreatedOn
)
{
    public static List<GetRoleByIdResult> Roles { get; set; } = new();

    public static implicit operator GetUserByIdResult(User user)
    {
        var result = new GetUserByIdResult(
            user.Id,
            user.Name,
            user.Email,
            user.UserName,
            user.CreatedOn
        );

        if (user.UserRoles.Any())
            foreach (var userRole in user.UserRoles)
                if (userRole != null)
                    Roles.Add(userRole.Role);

        return result;
    }
}
