namespace FastMeiliSync.API.Sources.EndPoints.V1;

public sealed class SourceModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var groupe = app.MapGroup(Router.SourceRoutes.V1.BASE_SOURCE_URL)
            .WithTags(nameof(Source).ToLower());
        groupe.MapPost(string.Empty, SourceEndpoints.HandleCreateAsync);
        groupe.MapPut(string.Empty, SourceEndpoints.HandleUpdateAsync);
        groupe.MapDelete(string.Empty, handler: SourceEndpoints.HandleDeleteByIdAsync);
        groupe.MapGet(string.Empty, SourceEndpoints.HandleGetByIdAsync);
    }
}
