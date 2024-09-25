namespace FastMeiliSync.Application.Features.Users.Commands.Delete;

public sealed record DeleteUserCommand(Guid Id) : IRequest<Response>;
