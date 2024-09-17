namespace FastMeiliSync.Application.Features.Syncs.Queries.GetById;

public sealed record GetSyncByIdQuery(Guid Id) : IRequest<Response>;
