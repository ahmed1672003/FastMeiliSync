namespace FastMeiliSync.Domain.Entities.Sources;

public sealed record Source : Entity<Guid>, ITrackableCreate, ITrackableDelete, ITrackableUpdate
{
    private readonly Dictionary<string, string> _configurations = new();

    private readonly List<Sync> _syncs = new();

    private Source() { }

    private Source(
        string label,
        string userId,
        string host,
        int port,
        string database,
        string url,
        SourceType type
    )
    {
        Id = Guid.NewGuid();
        Label = label;
        UserId = userId;
        Host = host;
        Port = port;
        Database = database;
        Url = url;
        Type = type;
    }

    public string Label { get; private set; }
    public string Host { get; private set; }
    public string UserId { get; private set; }
    public int? Port { get; private set; }
    public string Database { get; private set; }
    public string Url { get; private set; }
    public SourceType Type { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime? UpdatedOn { get; private set; }
    public DateTime? DeletedOn { get; private set; }
    public bool Deleted { get; private set; }
    public IReadOnlyDictionary<string, string> Configurations => _configurations;

    public IReadOnlyCollection<Sync> Syncs => _syncs;

    public static Source Create(
        string label,
        string userId,
        string host,
        int port,
        string database,
        string url,
        SourceType type
    ) => new(label, userId, host, port, database, url, type);

    public void Update(
        string label,
        string host,
        int? port,
        string database,
        string url,
        SourceType type
    )
    {
        if (string.IsNullOrEmpty(url))
        {
            ArgumentException.ThrowIfNullOrEmpty(label);
            ArgumentException.ThrowIfNullOrEmpty(host);
            ArgumentException.ThrowIfNullOrEmpty(database);
        }

        Label = label;
        Host = host;
        Port = port;
        Database = database;
        Url = url;
        Type = type;
    }

    public void AddConfigurations(IDictionary<string, string> configurations)
    {
        if (configurations.Any())
            foreach (var configuration in configurations)
                _configurations.Add(configuration.Key, configuration.Value);
    }

    public string GetConnectionString()
    {
        if (!string.IsNullOrEmpty(Url))
            return Url;

        StringBuilder connectionString = new();
        connectionString.Append($"{nameof(Host)}={Host};");
        connectionString.Append($"{nameof(Port)}={Port};");
        connectionString.Append($"{nameof(UserId)}={UserId};");
        connectionString.Append($"{nameof(Database)}={Database};");

        if (!_configurations.Any())
            return connectionString.ToString();

        foreach (var sourceConfiguration in _configurations)
            connectionString.Append($"{sourceConfiguration.Key}={sourceConfiguration.Value};");

        return connectionString.ToString();
    }

    public void SetCreatedOn()
    {
        CreatedOn = DateTime.UtcNow;
    }

    public void SetUpdatedOn()
    {
        UpdatedOn = DateTime.UtcNow;
    }

    public void SetDeletedOn()
    {
        Deleted = true;
        DeletedOn = DateTime.UtcNow;
    }

    public void Recover()
    {
        Deleted = false;
        DeletedOn = null;
    }
}
