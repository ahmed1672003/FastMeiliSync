using FastMeiliSync.Domain.Entities.Users;

namespace FastMeiliSync.API.MeiliSearches.Endpoints.V1;

public sealed class UserModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var groupe = app.MapGroup(Router.UserRoutes.V1.BASE_USER_URL)
            .WithTags(nameof(User).ToLower());

        groupe.MapPost(string.Empty, UserEndpoints.HandleCreateAsync).RequireAuthorization();
        groupe.MapPost(Router.UserRoutes.V1.Seed, UserEndpoints.HandleSeedAsync).AllowAnonymous();
        groupe
            .MapPatch(Router.UserRoutes.V1.Logout, UserEndpoints.HandleLogoutAsync)
            .RequireAuthorization();
        groupe.MapPut(Router.UserRoutes.V1.LogIn, UserEndpoints.HandleLoginAsync).AllowAnonymous();
        groupe.MapDelete(string.Empty, UserEndpoints.HandleDeleteByIdAsync).RequireAuthorization();
        groupe.MapGet(string.Empty, UserEndpoints.HandleGetByIdAsync).RequireAuthorization();
        groupe.MapPut(string.Empty, UserEndpoints.HandleUpdateAsync).RequireAuthorization();
        groupe
            .MapGet(Router.UserRoutes.V1.Paginate, UserEndpoints.HandlePaginateAsync)
            .RequireAuthorization();
    }
}
