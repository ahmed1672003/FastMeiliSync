using FastMeiliSync.Domain.Entities.Tokens;

namespace FastMeiliSync.Infrastructure.Repositories;

public sealed class TokenRepository : Repository<Token, Guid>, ITokenRepository
{
    public TokenRepository(IMeiliSyncDbContext meiliSyncDbContext)
        : base(meiliSyncDbContext) { }

    public async Task DeactivateTokneAsync(User user, CancellationToken cancellationToken = default)
    {
        var token = await _entities.FirstAsync(x => x.UserId == user.Id);
        _entities.Remove(token);
    }
}
