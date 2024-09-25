using FastMeiliSync.Application.Features.Sources.Queries.Paginate;
using FastMeiliSync.Shared.Enums;
using static FastMeiliSync.Application.Features.Sources.Queries.Paginate.PaginateSourceQuery;

namespace FastMeiliSync.API.Sources.EndPoints.V1;

public sealed class SourceEndpoints
{
    /// <summary>
    /// add new data base source
    /// </summary>
    /// <remarks>
    /// First request:
    ///
    ///     {
    ///       "label": "meili sync",
    ///       "type": 0,
    ///       "url": "Data Source=SQL5111.site4now.net;Initial Catalog=db_a9f9f8_masa;User Id=db_a9f9f8_masa_admin;Password=ahmedadel2102023"
    ///     }
    ///
    ///     Required Fields: [label , type , url]
    ///
    /// Response:
    ///
    ///     {
    ///       "success": true,
    ///       "statusCode": 200,
    ///       "message": "operation done successfully",
    ///       "result": {
    ///         "id": "16f29533-ab82-457a-85e8-e74e549e649e",
    ///         "label": "meili sync",
    ///         "connectionString": "Data Source=SQL5111.site4now.net;Initial Catalog=db_a9f9f8_masa;User Id=db_a9f9f8_masa_admin;Password=ahmedadel2102023"
    ///         "createdOn": "2024-09-15T21:29:55.3645185Z"
    ///       }
    ///     }
    /// </remarks>
    /// <param name="command"></param>
    /// <param name="sender"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<IResult> HandleCreateAsync(
        CreateSourceCommand command,
        ISender sender,
        CancellationToken cancellationToken = default
    ) => Results.Ok(await sender.Send(command, cancellationToken));

    /// <summary>
    /// update data base source
    /// </summary>
    /// <remarks>
    ///
    /// First request:
    ///
    ///     {
    ///       "id": "16f29533-ab82-457a-85e8-e74e549e649e",
    ///       "label": "meili sync",
    ///       "type": 0,
    ///       "url": "Data Source=SQL5111.site4now.net;Initial Catalog=db_a9f9f8_masa;User Id=db_a9f9f8_masa_admin;Password=ahmedadel2102023"
    ///     }
    ///
    ///     Required Fields: [id, label , type , url]
    ///
    /// Response:
    ///
    ///     {
    ///       "success": true,
    ///       "statusCode": 200,
    ///       "message": "operation done successfully",
    ///       "result": {
    ///         "id": "16f29533-ab82-457a-85e8-e74e549e649e",
    ///         "label": "meili sync",
    ///         "connectionString": "Data Source=SQL5111.site4now.net;Initial Catalog=db_a9f9f8_masa;User Id=db_a9f9f8_masa_admin;Password=ahmedadel2102023"
    ///         "createdOn": "2024-09-15T21:29:55.3645185Z"
    ///       }
    ///     }
    /// </remarks>
    /// <param name="command"></param>
    /// <param name="sender"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<IResult> HandleUpdateAsync(
        UpdateSourceCommand command,
        ISender sender,
        CancellationToken cancellationToken = default
    ) => Results.Ok(await sender.Send(command, cancellationToken));

    /// <summary>
    /// delete specific data base source
    /// </summary>
    /// <param name="id"></param>
    /// <param name="sender"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<IResult> HandleDeleteByIdAsync(
        Guid id,
        ISender sender,
        CancellationToken cancellationToken = default
    ) => Results.Ok(await sender.Send(new DeleteSourceByIdCommand(id), cancellationToken));

    /// <summary>
    /// get specific data base source
    /// </summary>
    /// <param name="id"></param>
    /// <param name="sender"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<IResult> HandleGetByIdAsync(
        Guid id,
        ISender sender,
        CancellationToken cancellationToken = default
    ) => Results.Ok(await sender.Send(new GetSourceByIdQuery(id), cancellationToken));

    /// <summary>
    /// paginate sources
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
        SourceOrderBy orderBy = SourceOrderBy.CreatedOn,
        OrderByDirection orderByDirection = OrderByDirection.Ascending,
        CancellationToken cancellationToken = default
    ) =>
        Results.Ok(
            await sender.Send(
                new PaginateSourceQuery(
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
