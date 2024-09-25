using FastMeiliSync.Domain.Entities.Tokens;

namespace FastMeiliSync.Infrastructure.Repositories;

public sealed class TokenRepository : Repository<Token, Guid>, ITokenRepository
{
    public TokenRepository(IMeiliSyncDbContext meiliSyncDbContext)
        : base(meiliSyncDbContext) { }
}
