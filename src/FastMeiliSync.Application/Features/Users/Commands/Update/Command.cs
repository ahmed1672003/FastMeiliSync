namespace FastMeiliSync.Application.Features.Users.Commands.Update;

public sealed record UpdateUserCommand(
    Guid Id,
    string Name,
    string UserName,
    string Email,
    List<Guid> RoleIds
) : IRequest<Response>;
