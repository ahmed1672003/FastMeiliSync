﻿using FastMeiliSync.Application.Features.MeiliSearches.Queries.Paginate;
using FastMeiliSync.Shared.Enums;
using static FastMeiliSync.Application.Features.MeiliSearches.Queries.Paginate.PaginateMeiliSearchQuery;

namespace FastMeiliSync.API.MeiliSearches.Endpoints.V1;

public sealed class MeiliSearchEndpoints
{
    /// <summary>
    /// add new meili~search instance
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     {
    ///       "label": "meili search instance",
    ///       "url": "http://localhost:7700/",
    ///       "apiKey": "Afk3C73hStvnUV69xTaPdHwBM4g02lm1MWeeaRJ49vo"
    ///     }
    ///
    /// Required Fields:
    ///
    ///     [label , url , apiKey]
    ///
    /// Response:
    ///
    ///     {
    ///       "success": true,
    ///       "statusCode": 200,
    ///       "message": operation done successfully,
    ///       "result": {
    ///         "id": "e59fcc76-4d72-4ca8-a9dd-3b411d606c5c",
    ///         "label": "meili search instance",
    ///         "apiKey": "Afk3C73hStvnUV69xTaPdHwBM4g02lm1MWeeaRJ49vo",
    ///         "url": "http://localhost:7700/",
    ///         "createdOn": "2024-09-15T21:29:55.3645185Z"
    ///       }
    ///     }
    /// </remarks>
    /// <param name="command"></param>
    /// <param name="sender"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<IResult> HandleCreateAsync(
        CreateMeiliSearchCommand command,
        ISender sender,
        CancellationToken cancellationToken = default
    ) => Results.Ok(await sender.Send(command, cancellationToken));

    /// <summary>
    /// update meili~search instance
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     {
    ///       "id": "e59fcc76-4d72-4ca8-a9dd-3b411d606c5c",
    ///       "label": "meili search instance",
    ///       "url": "http://localhost:7700/",
    ///       "apiKey": "Afk3C73hStvnUV69xTaPdHwBM4g02lm1MWeeaRJ49vo"
    ///     }
    ///
    /// Required Fields:
    ///
    ///     [id, label , url , apiKey]
    ///
    /// Response:
    ///
    ///     {
    ///       "success": true,
    ///       "statusCode": 200,
    ///       "message": operation done successfully,
    ///       "result": {
    ///         "id": "e59fcc76-4d72-4ca8-a9dd-3b411d606c5c",
    ///         "label": "meili search instance",
    ///         "apiKey": "Afk3C73hStvnUV69xTaPdHwBM4g02lm1MWeeaRJ49vo",
    ///         "url": "http://localhost:7700/",
    ///         "createdOn": "2024-09-15T21:29:55.3645185Z"
    ///       }
    ///     }
    /// </remarks>
    /// <param name="command"></param>
    /// <param name="sender"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<IResult> HandleUpdateAsync(
        UpdateMeiliSearchCommand command,
        ISender sender,
        CancellationToken cancellationToken = default
    ) => Results.Ok(await sender.Send(command, cancellationToken));

    /// <summary>
    /// delete meili~search instance
    /// </summary>
    /// <param name="id"></param>
    /// <param name="sender"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<IResult> HandleDeleteByIdAsync(
        Guid id,
        ISender sender,
        CancellationToken cancellationToken = default
    ) => Results.Ok(await sender.Send(new DeleteMeiliSearchByIdCommand(id), cancellationToken));

    /// <summary>
    /// get specific meili-search instance
    /// </summary>
    /// <param name="id"></param>
    /// <param name="sender"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<IResult> HandleGetByIdAsync(
        Guid id,
        ISender sender,
        CancellationToken cancellationToken = default
    ) => Results.Ok(await sender.Send(new GetMeiliSearchByIdQuery(id), cancellationToken));

    /// <summary>
    ///  paginate meili-search instances
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="search"></param>
    /// <param name="orderBeforPagination"></param>
    /// <param name="orderBy"></param>
    /// <param name="orderByDirection"></param>
    /// <param name="sender"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<IResult> HandlePaginateAsync(
        ISender sender,
        int pageNumber = 1,
        int pageSize = 10,
        string search = "",
        bool orderBeforPagination = true,
        MeiliSearchOrderBy orderBy = MeiliSearchOrderBy.CreatedOn,
        OrderByDirection orderByDirection = OrderByDirection.Ascending,
        CancellationToken cancellationToken = default
    ) =>
        Results.Ok(
            await sender.Send(
                new PaginateMeiliSearchQuery(
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
