namespace FastMeiliSync.Application.Features.Roles.Queries.GetById;

public sealed record GetRoleByIdQuery(Guid Id) : IRequest<Response>;
