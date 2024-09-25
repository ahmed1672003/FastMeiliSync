using System.Security.Claims;
using FastMeiliSync.Domain.Entities.Users;

namespace FastMeiliSync.Domain.Abstraction;

public interface IJWTManager
{
    Task<string> GenerateTokenAsync(
        User user,
        Func<User, List<Claim>> getClaims,
        CancellationToken cancellationToken = default
    );
}
