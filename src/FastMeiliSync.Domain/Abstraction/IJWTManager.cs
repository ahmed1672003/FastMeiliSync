using FastMeiliSync.Domain.Entities.Users;

namespace FastMeiliSync.Domain.Abstraction;

public interface IJWTManager
{
    Task<string> GenerateTokenAsync(User user, CancellationToken cancellationToken = default);
}
