using FastMeiliSync.Application.Features.Roles.Queries.GetById;
using FastMeiliSync.Application.Features.Syncs.Queries.Paginate;
using FastMeiliSync.Application.Features.Users.Commands.Create;
using FastMeiliSync.Application.Features.Users.Commands.Delete;
using FastMeiliSync.Application.Features.Users.Commands.Login;
using FastMeiliSync.Application.Features.Users.Commands.Logout;
using FastMeiliSync.Application.Features.Users.Commands.Seed;
using FastMeiliSync.Application.Features.Users.Commands.Update;
using FastMeiliSync.Shared.Enums;
using static FastMeiliSync.Application.Features.Syncs.Queries.Paginate.PaginateUserQuery;

namespace FastMeiliSync.API.MeiliSearches.Endpoints.V1;

public sealed class UserEndpoints
{
    public static async Task<IResult> HandleSeedAsync(
        ISender sender,
        CancellationToken cancellationToken = default
    ) => Results.Ok(await sender.Send(new SeedUsersCommand(), cancellationToken));

    public static async Task<IResult> HandleCreateAsync(
        CreateUserCommand command,
        ISender sender,
        CancellationToken cancellationToken = default
    ) => Results.Ok(await sender.Send(command, cancellationToken));

    public static async Task<IResult> HandleLoginAsync(
        LogInUserCommand command,
        ISender sender,
        CancellationToken cancellationToken = default
    ) => Results.Ok(await sender.Send(command, cancellationToken));

    public static async Task<IResult> HandleLogoutAsync(
        ISender sender,
        CancellationToken cancellationToken = default
    ) => Results.Ok(await sender.Send(new LogoutUserCommand(), cancellationToken));

    public static async Task<IResult> HandleUpdateAsync(
        UpdateUserCommand command,
        ISender sender,
        CancellationToken cancellationToken = default
    ) => Results.Ok(await sender.Send(command, cancellationToken));

    public static async Task<IResult> HandleDeleteByIdAsync(
        Guid id,
        ISender sender,
        CancellationToken cancellationToken = default
    ) => Results.Ok(await sender.Send(new DeleteUserCommand(id), cancellationToken));

    public static async Task<IResult> HandleGetByIdAsync(
        Guid id,
        ISender sender,
        CancellationToken cancellationToken = default
    ) => Results.Ok(await sender.Send(new GetUserByIdQuery(id), cancellationToken));

    public static async Task<IResult> HandlePaginateAsync(
        ISender sender,
        int pageNumber = 1,
        int pageSize = 10,
        string search = "",
        bool orderBeforPagination = true,
        UserOrderBy orderBy = UserOrderBy.CreatedOn,
        OrderByDirection orderByDirection = OrderByDirection.Ascending,
        CancellationToken cancellationToken = default
    ) =>
        Results.Ok(
            await sender.Send(
                new PaginateUserQuery(
                    pageNumber,
                    pageSize,
                    search,
                    orderBeforPagination,
                    orderBy,
                    orderByDirection
                ),
                cancellationToken
            )
        );
}
