using FastMeiliSync.Domain.Entities.Users;

namespace FastMeiliSync.Domain.Entities.Tokens;

public sealed record Token : Entity<Guid>, ITrackableCreate, ITrackableUpdate
{
    private Token() { }

    private Token(Guid userId, string content)
    {
        Content = content;
        UserId = userId;
    }

    public string Content { get; private set; }
    public DateTime? UpdatedOn { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public Guid UserId { get; private set; }
    public User User { get; private set; }

    public static Token Create(Guid userId, string content) => new(userId, content);

    public void Update(string content)
    {
        Content = content;
    }

    public void SetUpdatedOn()
    {
        UpdatedOn = DateTime.UtcNow;
    }

    public void SetCreatedOn()
    {
        CreatedOn = DateTime.UtcNow;
    }
}
