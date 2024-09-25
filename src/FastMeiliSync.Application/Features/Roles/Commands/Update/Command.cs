namespace FastMeiliSync.Application.Features.Roles.Commands.Update;

public sealed record UpdateRoleCommand(Guid Id, string Name) : IRequest<Response>;
