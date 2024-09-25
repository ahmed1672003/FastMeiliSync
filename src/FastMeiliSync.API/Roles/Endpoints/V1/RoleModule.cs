using FastMeiliSync.Domain.Entities.Roles;

namespace FastMeiliSync.API.MeiliSearches.Endpoints.V1;

public sealed class RoleModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var groupe = app.MapGroup(Router.RoleRoutes.V1.BASE_ROLE_URL)
            .WithTags(nameof(Role).ToLower());
        groupe.MapPost(string.Empty, RoleEndpoints.HandleCreateAsync);
        groupe.MapPut(string.Empty, RoleEndpoints.HandleUpdateAsync);
        groupe.MapDelete(string.Empty, RoleEndpoints.HandleDeleteByIdAsync);
        groupe.MapGet(string.Empty, RoleEndpoints.HandleGetByIdAsync);
        groupe.MapGet(Router.RoleRoutes.V1.Paginate, RoleEndpoints.HandlePaginateAsync);
    }
}
