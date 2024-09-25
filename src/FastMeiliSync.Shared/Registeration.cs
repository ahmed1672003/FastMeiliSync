namespace FastMeiliSync.Shared;

public static class Registeration
{
    public static IServiceCollection RegisterSharedDepenedncies(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddScoped<IUserContext, UserContext>();

        return services;
    }
}
