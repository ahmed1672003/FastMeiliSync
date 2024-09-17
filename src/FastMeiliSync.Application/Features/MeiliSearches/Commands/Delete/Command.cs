namespace FastMeiliSync.Application.Features.MeiliSearches.Commands.Delete;

public sealed record DeleteMeiliSearchByIdCommand(Guid Id) : IRequest<Response>;
