namespace FastMeiliSync.Application.Features.MeiliSearches.Commands.Create;

public sealed record CreateMeiliSearchCommand : IRequest<Response>
{
    public CreateMeiliSearchCommand(string label, string url, string apiKey)
    {
        Label = label;
        Url = url;
        ApiKey = apiKey;
    }

    public string Label { get; set; }
    public string Url { get; set; }
    public string ApiKey { get; set; }

    public static implicit operator MeiliSearch(CreateMeiliSearchCommand command)
    {
        var meiliSearch = MeiliSearch.Create(command.Label, command.Url, command.ApiKey);
        return meiliSearch;
    }
}
