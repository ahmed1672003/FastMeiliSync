using FastMeiliSync.Domain.Entities.Users;

namespace FastMeiliSync.Domain.Entities.Tokens;

public interface ITokenRepository : IRepository<Token, Guid>
{
    Task DeactivateTokneAsync(User user, CancellationToken cancellationToken = default);
}
