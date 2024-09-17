namespace FastMeiliSync.Application.Features.Sources.Queries.GetById;

public sealed record GetSourceByIdResult
{
    public GetSourceByIdResult(Guid id, string label, string connectionString)
    {
        Id = id;
        Label = label;
        ConnectionString = connectionString;
    }

    public Guid Id { get; set; }
    public string Label { get; set; }
    public string ConnectionString { get; set; }

    public static implicit operator GetSourceByIdResult(Source source)
    {
        GetSourceByIdResult result = new GetSourceByIdResult(source.Id, source.Label, source.Url);
        if (!string.IsNullOrEmpty(result.ConnectionString))
        {
            return result;
        }
        result = result with { ConnectionString = source.GetConnectionString() };
        return result;
    }
}
