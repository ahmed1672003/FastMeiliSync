namespace FastMeiliSync.Application.Features.Sources.Queries.GetById;

public sealed record GetSourceByIdQuery(Guid Id) : IRequest<Response>;
