using FastMeiliSync.Domain.Entities.TableSources;

namespace FastMeiliSync.Domain.Entities.Tables;

public record Table : Entity<Guid>, ITrackableCreate, ITrackableDelete, ITrackableUpdate
{
    private readonly HashSet<TableSource> _tableSources = new();

    private Table() { }

    private Table(string name, string schema)
    {
        Id = Guid.NewGuid();
        Name = name;
        Schema = schema;
    }

    public string Name { get; private set; }
    public string Schema { get; private set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public bool Deleted { get; set; }
    public IReadOnlyCollection<TableSource> TableSources => _tableSources;

    public static Table Create(string name, string schema) => new(name, schema);

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
