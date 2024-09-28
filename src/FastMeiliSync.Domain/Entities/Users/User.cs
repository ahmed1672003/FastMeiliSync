using FastMeiliSync.Domain.Entities.Tokens;
using FastMeiliSync.Domain.Entities.UsersRoles;
using Microsoft.AspNetCore.Identity;

namespace FastMeiliSync.Domain.Entities.Users;

public sealed record User : Entity<Guid>, ITrackableCreate, ITrackableUpdate
{
    private readonly List<UserRole> _userRoles = new();

    private User(string name, string userName, string email, bool master)
    {
        Name = name;
        UserName = userName;
        Email = email;
        Master = master;
    }

    private User() { }

    public string Name { get; private set; }
    public string UserName { get; private set; }
    public string Email { get; private set; }
    public string HashedPassword { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime? UpdatedOn { get; private set; }
    public bool Master { get; private set; }
    public Token Token { get; private set; }

    public IReadOnlyCollection<UserRole> UserRoles => _userRoles;

    public void UpdateProfile(string name, string userName, string email)
    {
        Name = name;
        UserName = userName;
        Email = email;
    }

    public void AssignToRoles(List<Guid> roleIds)
    {
        if (roleIds == null)
            ArgumentNullException.ThrowIfNull(roleIds);
        _userRoles.Clear();

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

    public static User Create(string name, string userName, string email, bool master) =>
        new User(name, userName, email, master);

    public void ChangeToken(string token)
    {
        if (Token == null)
            return;
        Token.Update(token);
    }

    public void AddToken(string token)
    {
        Token = Token.Create(Id, token);
    }

    public void HashPassword(IPasswordHasher<User> passwordHasher, string password)
    {
        HashedPassword = passwordHasher.HashPassword(this, password);
    }
}
