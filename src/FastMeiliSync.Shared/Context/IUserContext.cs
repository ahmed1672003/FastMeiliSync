namespace FastMeiliSync.Shared.Context;

public interface IUserContext
{
    (Guid Id, bool Exist) UserId { get; }
}
