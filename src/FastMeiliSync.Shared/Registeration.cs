using Hangfire;
using Hangfire.PostgreSql;

namespace FastMeiliSync.Shared;

public static class Registeration
{
    public static IServiceCollection RegisterSharedDepenedncies(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddScoped<IUserContext, UserContext>();
        services.AddHangfire(config =>
        {
            config
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(c =>
                    c.UseNpgsqlConnection(
                        configuration.GetConnectionString("MEILI_SYNC_CONNECTION")
                    )
                );
        });
        return services;
    }
}
