namespace FastMeiliSync.Domain.Entities.Syncs;

public sealed record Sync : Entity<Guid>, ITrackableCreate, ITrackableUpdate, ITrackableDelete
{
    private Sync() { }

    private Sync(string label, Guid sourceId, Guid meiliSearchId)
    {
        Id = Guid.NewGuid();
        Label = label;
        SourceId = sourceId;
        MeiliSearchId = meiliSearchId;
    }

    public string Label { get; set; }
    public Guid SourceId { get; private set; }
    public Guid MeiliSearchId { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime? UpdatedOn { get; private set; }
    public DateTime? DeletedOn { get; private set; }
    public bool Deleted { get; private set; }
    public Source Source { get; private set; }
    public MeiliSearch MeiliSearch { get; private set; }

    public static Sync Create(string label, Guid sourceId, Guid meiliSearchId)
    {
        return new Sync(label, sourceId, meiliSearchId);
    }

    public void Update(string label, Guid sourceId, Guid meiliSearchId)
    {
        Label = label;
        SourceId = sourceId;
        MeiliSearchId = meiliSearchId;
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
