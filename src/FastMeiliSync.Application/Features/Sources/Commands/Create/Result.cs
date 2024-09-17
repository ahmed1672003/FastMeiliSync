namespace FastMeiliSync.Application.Features.Sources.Commands.Create;

public sealed record CreateSourceResult
{
    public CreateSourceResult(Guid id, string label, string connectionString)
    {
        Id = id;
        Label = label;
        ConnectionString = connectionString;
    }

    public Guid Id { get; set; }
    public string Label { get; set; }
    public string ConnectionString { get; set; }

    public static implicit operator CreateSourceResult(Source source)
    {
        CreateSourceResult result = new CreateSourceResult(source.Id, source.Label, source.Url);
        if (!string.IsNullOrEmpty(result.ConnectionString))
        {
            return result;
        }
        result = result with { ConnectionString = source.GetConnectionString() };
        return result;
    }
}
