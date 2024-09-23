using FastMeiliSync.Domain.Entities.Roles;
using FastMeiliSync.Domain.Entities.Users;

namespace FastMeiliSync.Domain.Entities.UsersRoles;

public sealed record UserRole : Entity<Guid>, ITrackableCreate, ITrackableUpdate
{
    private UserRole(Guid roleId, Guid userId)
    {
        RoleId = roleId;
        UserId = userId;
    }

    private UserRole() { }

    public Guid RoleId { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime? UpdatedOn { get; private set; }
    public User User { get; private set; }
    public Role Role { get; private set; }

    public void SetUpdatedOn()
    {
        UpdatedOn = DateTime.UtcNow;
    }

    public void SetCreatedOn()
    {
        CreatedOn = DateTime.UtcNow;
    }

    public static UserRole Create(Guid roleId, Guid userId) => new(roleId, userId);
}
