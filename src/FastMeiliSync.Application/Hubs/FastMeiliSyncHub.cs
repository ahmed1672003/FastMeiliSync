using FastMeiliSync.Application.Abstractions;
using Microsoft.AspNetCore.SignalR;

namespace FastMeiliSync.Application.Hubs;

public sealed class FastMeiliSyncHub : Hub<IFastMeiliSyncHubClient> { }
