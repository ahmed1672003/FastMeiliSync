namespace FastMeiliSync.Application.Features.Users.Commands.Create;

public sealed record CreateUserCommand(
    string Name,
    string UserName,
    string Email,
    string Password,
    string ConfirmedPassword,
    List<Guid> RoleIds
) : IRequest<Response>
{
    public static implicit operator User(CreateUserCommand command) =>
        User.Create(command.Name, command.UserName, command.Email, false);
}
