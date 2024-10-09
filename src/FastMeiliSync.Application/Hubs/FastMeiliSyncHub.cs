using FastMeiliSync.Application.Abstractions;
using FastMeiliSync.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace FastMeiliSync.Application.Hubs;

public sealed class FastMeiliSyncHub : Hub<IFastMeiliSyncHubClient>
{
    readonly IHttpContextAccessor _httpContextAccessor;
    readonly IMeiliSyncDbContext _meiliSyncDbContext;

    public FastMeiliSyncHub(
        IHttpContextAccessor httpContextAccessor,
        IMeiliSyncDbContext meiliSyncDbContext
    )
    {
        _httpContextAccessor = httpContextAccessor;
        _meiliSyncDbContext = meiliSyncDbContext;
    }

    public override async Task OnConnectedAsync()
    {
        var ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();
        var _sessions = _meiliSyncDbContext.Set<Session>();
        await _sessions.AddAsync(
            new()
            {
                Active = true,
                IpAddress = ipAddress,
                ConnectionId = Context.ConnectionId
            }
        );
        await _meiliSyncDbContext.SaveChangesAsync(1);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var _sessions = _meiliSyncDbContext.Set<Session>();

        var session = await _sessions.FirstOrDefaultAsync(x =>
            x.ConnectionId == Context.ConnectionId
        );

        if (session != null)
        {
            session.Active = false;
            await _meiliSyncDbContext.SaveChangesAsync(1);
        }
        await base.OnDisconnectedAsync(exception);
    }
}
