namespace FastMeiliSync.Application.Features.Users.Commands.Create;

public sealed record LogInUserResult(Guid UserId, string Token)
{
    public static implicit operator LogInUserResult(User user) => new(user.Id, user.Token.Content);
}
