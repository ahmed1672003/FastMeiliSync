namespace FastMeiliSync.Shared.Context;

public interface IUserContext
{
    (Guid Value, bool Exist) UserId { get; }
}
