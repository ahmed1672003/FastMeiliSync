namespace FastMeiliSync.Application.Features.Sources.Commands.Delete;

public sealed record DeleteSourceByIdCommand(Guid Id) : IRequest<Response>;
