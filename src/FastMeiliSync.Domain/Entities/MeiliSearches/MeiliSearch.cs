namespace FastMeiliSync.Domain.Entities.MeiliSearches;

public sealed record MeiliSearch : Entity<Guid>, ITrackableCreate, ITrackableUpdate
{
    private readonly List<Sync> _syncs = new();

    private MeiliSearch() { }

    private MeiliSearch(string label, string url, string apiKey)
    {
        Id = Guid.NewGuid();
        Label = label;
        Url = url;
        ApiKey = apiKey;
    }

    public string Label { get; private set; }
    public string Url { get; private set; }
    public string ApiKey { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime? UpdatedOn { get; private set; }

    public IReadOnlyCollection<Sync> Syncs => _syncs;

    public void Update(string label, string url, string apiKey)
    {
        Label = label;
        Url = url;
        ApiKey = apiKey;
    }

    public static MeiliSearch Create(string label, string url, string apiKey)
    {
        return new MeiliSearch(label, url, apiKey);
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
