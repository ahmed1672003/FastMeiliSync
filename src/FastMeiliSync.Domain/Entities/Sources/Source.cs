namespace FastMeiliSync.Domain.Entities.Sources;

public sealed record Source : Entity<Guid>, ITrackableCreate, ITrackableUpdate
{
    private readonly List<Sync> _syncs = new();

    private Source() { }

    private Source(string label, string database, string url, SourceType type)
    {
        Id = Guid.NewGuid();
        Label = label;
        Url = url;
        Database = database;
        Type = type;
    }

    public string Label { get; private set; }
    public string Database { get; private set; }
    public string Url { get; private set; }
    public SourceType Type { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime? UpdatedOn { get; private set; }

    public IReadOnlyCollection<Sync> Syncs => _syncs;

    public static Source Create(string label, string database, string url, SourceType type) =>
        new(label, database, url, type);

    public void Update(string label, string database, string url, SourceType type)
    {
        Label = label;
        Database = database;
        Url = url;
        Type = type;
    }

    public void SetCreatedOn()
    {
        CreatedOn = DateTime.UtcNow;
    }

    public void SetUpdatedOn()
    {
        UpdatedOn = DateTime.UtcNow;
    }
}
