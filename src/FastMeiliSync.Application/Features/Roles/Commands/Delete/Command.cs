namespace FastMeiliSync.Application.Features.Roles.Commands.Delete;

public sealed record DeleteRoleCommand(Guid Id) : IRequest<Response>;
