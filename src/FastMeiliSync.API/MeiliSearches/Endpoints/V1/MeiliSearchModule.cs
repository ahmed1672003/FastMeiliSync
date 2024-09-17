using Carter;
using MeiliSync.BuildingBlock.Core.Router;
using MeiliSync.Domain.Entities.MeiliSearches;

namespace MeiliSync.API.MeiliSearches.Endpoints.V1;

public sealed class MeiliSearchModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var groupe = app.MapGroup(Router.MeiliSearchRoutes.V1.BASE_MEILISEARCH_URL)
            .WithTags(nameof(MeiliSearch).ToLower());
        groupe.MapPost(string.Empty, MeiliSearchEndpoints.HandleCreateAsync);
        groupe.MapPut(string.Empty, MeiliSearchEndpoints.HandleUpdateAsync);
        groupe.MapDelete(string.Empty, MeiliSearchEndpoints.HandleDeleteByIdAsync);
        groupe.MapGet(string.Empty, MeiliSearchEndpoints.HandleGetByIdAsync);
    }
}
