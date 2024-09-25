namespace FastMeiliSync.Application.Features.Roles.Queries.GetById;

public sealed record GetUserByIdQuery(Guid Id) : IRequest<Response>;
