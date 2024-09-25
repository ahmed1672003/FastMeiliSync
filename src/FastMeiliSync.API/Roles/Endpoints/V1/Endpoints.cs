using FastMeiliSync.Application.Features.Roles.Commands.Create;
using FastMeiliSync.Application.Features.Roles.Commands.Delete;
using FastMeiliSync.Application.Features.Roles.Commands.Update;
using FastMeiliSync.Application.Features.Roles.Queries.GetById;
using FastMeiliSync.Application.Features.Roles.Queries.Paginate;
using FastMeiliSync.Shared.Enums;
using static FastMeiliSync.Application.Features.Roles.Queries.Paginate.PaginateRoleQuery;

namespace FastMeiliSync.API.MeiliSearches.Endpoints.V1;

public sealed class RoleEndpoints
{
    public static async Task<IResult> HandleCreateAsync(
        CreateRoleCommand command,
        ISender sender,
        CancellationToken cancellationToken = default
    ) => Results.Ok(await sender.Send(command, cancellationToken));

    public static async Task<IResult> HandleUpdateAsync(
        UpdateRoleCommand command,
        ISender sender,
        CancellationToken cancellationToken = default
    ) => Results.Ok(await sender.Send(command, cancellationToken));

    public static async Task<IResult> HandleDeleteByIdAsync(
        Guid id,
        ISender sender,
        CancellationToken cancellationToken = default
    ) => Results.Ok(await sender.Send(new DeleteRoleCommand(id), cancellationToken));

    public static async Task<IResult> HandleGetByIdAsync(
        Guid id,
        ISender sender,
        CancellationToken cancellationToken = default
    ) => Results.Ok(await sender.Send(new GetRoleByIdQuery(id), cancellationToken));

    public static async Task<IResult> HandlePaginateAsync(
        ISender sender,
        int pageNumber = 1,
        int pageSize = 10,
        string search = "",
        bool orderBeforPagination = true,
        RoleOrderBy orderBy = RoleOrderBy.CreatedOn,
        OrderByDirection orderByDirection = OrderByDirection.Ascending,
        CancellationToken cancellationToken = default
    ) =>
        Results.Ok(
            await sender.Send(
                new PaginateRoleQuery(
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
