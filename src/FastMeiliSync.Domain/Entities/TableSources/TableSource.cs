namespace FastMeiliSync.Domain.Entities.TableSources;

public sealed record TableSource
    : Entity<Guid>,
        ITrackableCreate,
        ITrackableUpdate,
        ITrackableDelete
{
    public TableSource(Guid sourceId, Guid tableId)
    {
        Id = Guid.NewGuid();
        SourceId = sourceId;
        TableId = tableId;
    }

    public Guid SourceId { get; private set; }
    public Guid TableId { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime? UpdatedOn { get; private set; }
    public DateTime? DeletedOn { get; private set; }
    public bool Deleted { get; private set; }

    public static TableSource Create(Guid sourceId, Guid tableId)
    {
        return new TableSource(sourceId, tableId);
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
