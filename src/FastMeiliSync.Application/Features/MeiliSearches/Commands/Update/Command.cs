namespace FastMeiliSync.Application.Features.MeiliSearches.Commands.Update;

public sealed record UpdateMeiliSearchCommand(Guid Id, string Label, string ApiKey, string Url)
    : IRequest<Response>;
