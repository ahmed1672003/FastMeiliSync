namespace FastMeiliSync.Domain.Abstraction;

public interface ITrackableCreate
{
    DateTime CreatedOn { get; }
    void SetCreatedOn();
}
