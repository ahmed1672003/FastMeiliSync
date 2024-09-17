using Carter;

namespace MeiliSync.API.Sources.Endpoints.V1;

public sealed class SyncModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var groupe = app.MapGroup(Router.SyncRoutes.V1.BASE_SYNC_URL)
            .WithTags(nameof(Sync).ToLower());
        groupe.MapPost(string.Empty, SyncEndpoints.HandleCreateAsync);
        groupe.MapPut(string.Empty, SyncEndpoints.HandleUpdateAsync);
        groupe.MapDelete(string.Empty, SyncEndpoints.HandleDeleteByIdAsync);
        groupe.MapGet(string.Empty, SyncEndpoints.HandleGetByIdAsync);
    }
}
