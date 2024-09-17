namespace FastMeiliSync.Application.Features.MeiliSearches.Queries.GetById;

public sealed record GetMeiliSearchByIdQuery(Guid Id) : IRequest<Response>;
