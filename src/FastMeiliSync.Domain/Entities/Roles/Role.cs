using FastMeiliSync.Domain.Entities.UsersRoles;

namespace FastMeiliSync.Domain.Entities.Roles;

public sealed record Role : Entity<Guid>, ITrackableCreate, ITrackableUpdate
{
    private readonly List<UserRole> _roleUsers = new();

    private Role() { }

    private Role(string name) => Name = name;

    public string Name { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime? UpdatedOn { get; private set; }

    public IReadOnlyCollection<UserRole> RoleUsers => _roleUsers;

    public static Role Create(string Name) => new(Name);

    public void SetUpdatedOn()
    {
        UpdatedOn = DateTime.UtcNow;
    }

    public void SetCreatedOn()
    {
        CreatedOn = DateTime.UtcNow;
    }
}
