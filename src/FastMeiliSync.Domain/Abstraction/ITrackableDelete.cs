namespace FastMeiliSync.Domain.Abstraction;

public interface ITrackableDelete
{
    DateTime? DeletedOn { get; }
    bool Deleted { get; }

    void SetDeletedOn();
    void Recover();
}
