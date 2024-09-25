using FastMeiliSync.Domain.Entities.Users;

namespace FastMeiliSync.Domain.Entities.Tokens;

public sealed record Token : Entity<Guid>, ITrackableCreate, ITrackableUpdate
{
    private Token() { }

    private Token(string content) => Content = content;

    public string Content { get; private set; }
    public DateTime? UpdatedOn { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public Guid UserId { get; private set; }
    public User User { get; private set; }

    public static Token Create(string content) => new(content);

    public void SetUpdatedOn()
    {
        UpdatedOn = DateTime.UtcNow;
    }

    public void SetCreatedOn()
    {
        CreatedOn = DateTime.UtcNow;
    }
}
