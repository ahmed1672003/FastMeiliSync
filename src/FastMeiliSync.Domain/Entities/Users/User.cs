using FastMeiliSync.Domain.Entities.UsersRoles;

namespace FastMeiliSync.Domain.Entities.Users;

public sealed record User : Entity<Guid>, ITrackableCreate, ITrackableUpdate
{
    private readonly List<UserRole> _userRoles = new();

    private User(string name, string userName, string email, string hashedPassword)
    {
        Name = name;
        UserName = userName;
        Email = email;
        HashedPassword = hashedPassword;
    }

    private User() { }

    public string Name { get; private set; }
    public string UserName { get; private set; }
    public string Email { get; private set; }
    public string HashedPassword { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime? UpdatedOn { get; private set; }

    public IReadOnlyCollection<UserRole> UserRoles => _userRoles;

    public void AssignToRoles(List<Guid> roleIds)
    {
        if (roleIds == null)
            ArgumentNullException.ThrowIfNull(roleIds);

        _userRoles.AddRange(roleIds.Select(roleId => UserRole.Create(roleId, Id)));
    }

    public void SetUpdatedOn()
    {
        UpdatedOn = DateTime.UtcNow;
    }

    public void SetCreatedOn()
    {
        CreatedOn = DateTime.UtcNow;
    }

    public static User Create(string name, string userName, string email, string hashedPassword) =>
        new User(name, userName, email, hashedPassword);
}
