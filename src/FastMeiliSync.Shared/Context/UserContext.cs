using Microsoft.AspNetCore.Http;

namespace FastMeiliSync.Shared.Context;

public sealed record UserContext : IUserContext
{
    readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor) =>
        _httpContextAccessor = httpContextAccessor;

    public (Guid Value, bool Exist) UserId
    {
        get
        {
            if (
                _httpContextAccessor.HttpContext != null
                && _httpContextAccessor.HttpContext.User.Claims is not null
                && _httpContextAccessor.HttpContext.User.Claims.Any()
            )
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(x =>
                    x.Type == nameof(UserId)
                );

                if (userId != null)
                {
                    return (Guid.Parse(userId.Value), true);
                }
                else
                    return (Guid.Empty, false);
            }
            else
                return (Guid.Empty, false);
        }
    }
}
