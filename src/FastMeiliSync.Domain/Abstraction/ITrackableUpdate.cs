namespace FastMeiliSync.Domain.Abstraction;

public interface ITrackableUpdate
{
    DateTime? UpdatedOn { get; }
    void SetUpdatedOn();
}
