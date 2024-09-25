namespace FastMeiliSync.Application.Features.Users.Commands.Login;

public sealed record LogInUserCommand(string Email, string Password) : IRequest<Response>;
