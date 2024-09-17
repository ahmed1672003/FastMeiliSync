namespace FastMeiliSync.Application.Features.Syncs.Commands.Delete;

public sealed record DeleteSyncByIdCommand(Guid Id) : IRequest<Response>;
