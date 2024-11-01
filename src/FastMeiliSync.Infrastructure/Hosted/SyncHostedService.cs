//using FastMeiliSync.Application.Abstractions;
//using FastMeiliSync.Infrastructure.SearchEngine.Index;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;

//namespace FastMeiliSync.Infrastructure.Hosted;

//public sealed class SyncHostedService : BackgroundService
//{
//    readonly IServiceProvider _serviceProvider;

//    public SyncHostedService(IServiceProvider serviceProvider) =>
//        _serviceProvider = serviceProvider;

//    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//    {
//        var scope = _serviceProvider.CreateScope();
//        var wal2JsonService = scope.ServiceProvider.GetService<IWal2JosnService>();
//        var unitOfWork = scope.ServiceProvider.GetService<IMeiliSyncUnitOfWork>();
//        var indexService = scope.ServiceProvider.GetService<IIndexService>();

//        var syncs = await unitOfWork.Syncs.GetAllAsync<Sync>(
//            s => s.Working,
//            includes: s => s.Include(x => x.Source).Include(x => x.MeiliSearch),
//            selector: s => s,
//            stopTracking: true
//        );
//        List<Task> tasks = new();
//        foreach (var sync in syncs)
//        {
//            tasks.Add(
//                wal2JsonService.StartReplicationConnectionAsync(
//                    sync.Source.Database,
//                    sync.Source.Url,
//                    sync.MeiliSearch.Url,
//                    stoppingToken
//                )
//            );
//        }
//        Task.WhenAll(tasks);
//    }
//}
