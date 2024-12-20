﻿using FastMeiliSync.Domain.Entities.Tokens;
using FastMeiliSync.Infrastructure.Context;
using FastMeiliSync.Infrastructure.Context.Interceptors;
using FastMeiliSync.Infrastructure.JWT;
using FastMeiliSync.Infrastructure.Repositories;
using FastMeiliSync.Infrastructure.SearchEngine.Document;
using FastMeiliSync.Infrastructure.SearchEngine.Index;
using FastMeiliSync.Infrastructure.UnitOfWorks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FastMeiliSync.Infrastructure;

public static class Registeration
{
    public static IServiceCollection RegiserInfrastructureDependencies(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContextPool<IMeiliSyncDbContext, MeiliSyncDbContext>(
            cfg =>
            {
                cfg.UseNpgsql(configuration.GetConnectionString("MEILI_SYNC_CONNECTION"))
                    .UseQueryTrackingBehavior(
                        QueryTrackingBehavior.NoTrackingWithIdentityResolution
                    );
                cfg.EnableSensitiveDataLogging().EnableDetailedErrors();
                cfg.AddInterceptors(new OnSaveChangesInterceptor());
            },
            MeiliSyncConstants.MAX_MEILISYNC_DBCONTEXT_POOL_SIZE
        );

        services
            .AddScoped<IMeiliSyncUnitOfWork, MeiliSyncUnitOfWork>()
            .AddScoped(typeof(IRepository<,>), typeof(Repository<,>))
            .AddScoped<IMeiliSearchRepository, MeiliSearchRepository>()
            .AddScoped<ISyncRepository, SyncRepository>()
            .AddScoped<ISourceRepository, SourceRepository>()
            .AddScoped<ITableRepository, TableRepository>()
            .AddScoped<ITableSourceRepository, TableSourceRepository>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IUserRoleRepository, UserRoleRepository>()
            .AddScoped<ITokenRepository, TokenRepository>()
            .AddScoped<IRoleRepository, RoleRepository>();

        services
            .AddScoped<IIndexService, IndexService>()
            .AddScoped<IDocumentService, DocumentService>()
            .AddScoped<IJWTManager, JWTManager>();
        //.AddScoped<IWal2JosnService, Wal2JosnService>();
        //.AddScoped<IRedisService, RedisService>();
        //services.AddSingleton<IConnectionMultiplexer>(sp =>
        //    ConnectionMultiplexer.Connect(
        //        configuration.GetConnectionString("Redis")!,
        //        cfg =>
        //        {
        //            cfg.AbortOnConnectFail = false;
        //        }
        //    )
        //);
        // services.AddHostedService<SyncHostedService>();
        return services;
    }
}
