namespace FastMeiliSync.Application.Features.Sources.Commands.Update;

public sealed record UpdateSourceResult
{
    public UpdateSourceResult(Guid id, string label, string connectionString)
    {
        Id = id;
        Label = label;
        ConnectionString = connectionString;
    }

    public Guid Id { get; set; }
    public string Label { get; set; }
    public string ConnectionString { get; set; }

    public static implicit operator UpdateSourceResult(Source source)
    {
        UpdateSourceResult result = new UpdateSourceResult(source.Id, source.Label, source.Url);
        if (!string.IsNullOrEmpty(result.ConnectionString))
        {
            return result;
        }
        result = result with { ConnectionString = source.GetConnectionString() };
        return result;
    }
}
