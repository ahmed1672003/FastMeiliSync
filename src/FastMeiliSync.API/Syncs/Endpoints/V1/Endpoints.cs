using FastMeiliSync.Application.Features.MeiliSearches.Queries.Paginate;
using FastMeiliSync.Shared.Enums;
using static FastMeiliSync.Application.Features.MeiliSearches.Queries.Paginate.PaginateSyncQuery;

namespace FastMeiliSync.API.Syncs.Endpoints.V1;

public sealed class SyncEndpoints
{
    /// <summary>
    /// add new sync
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     {
    ///       "label": "strsinsg",
    ///       "sourceId": "2fda1a02-4191-4408-a707-892c92b79b16",
    ///       "meiliSearchId": "4d44913b-893d-4bbc-8098-675c37668c60"
    ///     }
    ///
    /// Required Fields:
    ///
    ///     [label , sourceId , meilisearchId]
    ///
    /// Response:
    ///
    ///     {
    ///       "success": true,
    ///       "statusCode": 200,
    ///       "message": "operation done successfully",
    ///       "result": {
    ///         "id": "e496ed77-22d7-4ec7-b459-e0d5b33205ae",
    ///         "label": "strsinsg",
    ///         "createdOn": "2024-09-15T22:32:52.423397Z",
    ///         "meiliSearch": {
    ///           "id": "4d44913b-893d-4bbc-8098-675c37668c60",
    ///           "label": "meili search instance",
    ///           "apiKey": "Afk3C73hStvnUV69xTaPdHwBM4g02lm1MWeeaRJ49vo",
    ///           "url": "http://localhost:7700/",
    ///           "createdOn": "2024-09-15T22:32:39.162363Z"
    ///         },
    ///         "source": {
    ///            "id": "2fda1a02-4191-4408-a707-892c92b79b16",
    ///            "label": "meili sync",
    ///            "connectionString": "Data Source=SQL5111.site4now.net;Initial Catalog=db_a9f9f8_masa;User Id=db_a9f9f8_masa_admin;Password=ahmedadel2102023"
    ///            "createdOn": "2024-09-15T22:32:39.162363Z"
    ///          }
    ///        }
    ///     }
    /// </remarks>
    /// <param name="command"></param>
    /// <param name="sender"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ///
    public static async Task<IResult> HandleCreateAsync(
        CreateSyncCommand command,
        ISender sender,
        CancellationToken cancellationToken = default
    ) => Results.Ok(await sender.Send(command, cancellationToken));

    /// <summary>
    /// update sync
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     {
    ///       "id": "2fda1a02-4191-4408-a707-892c92b79b16",
    ///       "label": "strsinsg",
    ///       "sourceId": "2fda1a02-4191-4408-a707-892c92b79b16",
    ///       "meiliSearchId": "4d44913b-893d-4bbc-8098-675c37668c60"
    ///     }
    ///
    /// Required Fields:
    ///
    ///     [id ,label , sourceId , meilisearchId]
    ///
    /// Response:
    ///
    ///     {
    ///       "success": true,
    ///       "statusCode": 200,
    ///       "message": "operation done successfully",
    ///       "result": {
    ///         "id": "e496ed77-22d7-4ec7-b459-e0d5b33205ae",
    ///         "label": "strsinsg",
    ///         "createdOn": "2024-09-15T22:32:52.423397Z",
    ///         "meiliSearch": {
    ///           "id": "4d44913b-893d-4bbc-8098-675c37668c60",
    ///           "label": "meili search instance",
    ///           "apiKey": "Afk3C73hStvnUV69xTaPdHwBM4g02lm1MWeeaRJ49vo",
    ///           "url": "http://localhost:7700/",
    ///           "createdOn": "2024-09-15T22:32:39.162363Z"
    ///         },
    ///         "source": {
    ///            "id": "2fda1a02-4191-4408-a707-892c92b79b16",
    ///            "label": "meili sync",
    ///            "connectionString": "Data Source=SQL5111.site4now.net;Initial Catalog=db_a9f9f8_masa;User Id=db_a9f9f8_masa_admin;Password=ahmedadel2102023"
    ///            "createdOn": "2024-09-15T21:29:55.3645185Z"
    ///          }
    ///        }
    ///     }
    /// </remarks>
    /// <param name="command"></param>
    /// <param name="sender"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ///
    public static async Task<IResult> HandleUpdateAsync(
        UpdateSyncCommand command,
        ISender sender,
        CancellationToken cancellationToken = default
    ) => Results.Ok(await sender.Send(command, cancellationToken));

    /// <summary>
    /// delete specific sync
    /// </summary>
    /// <param name="id"></param>
    /// <param name="sender"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<IResult> HandleDeleteByIdAsync(
        Guid id,
        ISender sender,
        CancellationToken cancellationToken = default
    ) => Results.Ok(await sender.Send(new DeleteSyncByIdCommand(id), cancellationToken));

    /// <summary>
    /// get specific sync
    /// </summary>
    /// <param name="id"></param>
    /// <param name="sender"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<IResult> HandleGetByIdAsync(
        Guid id,
        ISender sender,
        CancellationToken cancellationToken = default
    ) => Results.Ok(await sender.Send(new GetSyncByIdQuery(id), cancellationToken));

    /// <summary>
    /// paginate sync
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="search"></param>
    /// <param name="orderBeforPagination"></param>
    /// <param name="orderBy"></param>
    /// <param name="orderByDirection"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<IResult> HandlePaginateAsync(
        ISender sender,
        int pageNumber = 1,
        int pageSize = 10,
        string search = "",
        bool orderBeforPagination = true,
        SyncOrderBy orderBy = SyncOrderBy.CreatedOn,
        OrderByDirection orderByDirection = OrderByDirection.Ascending,
        CancellationToken cancellationToken = default
    ) =>
        Results.Ok(
            await sender.Send(
                new PaginateSyncQuery(
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
