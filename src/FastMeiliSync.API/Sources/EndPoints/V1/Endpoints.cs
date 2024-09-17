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
    ///       "user": "postgres",
    ///       "host": "localhost",
    ///       "port": 3000,
    ///       "database": "meilisync",
    ///       "type": 0,
    ///       "configurations": {
    ///         "sslmode": "prefer",
    ///         "timeout": "30"
    ///       }
    ///     }
    ///
    ///     Required Fields: [label , user , host , port , database , type]
    ///
    /// Second request:
    ///
    ///     {
    ///       "label": "meili sync",
    ///       "type": 0,
    ///       "url": "Data Source=SQL5111.site4now.net;Initial Catalog=db_a9f9f8_masa;User Id=db_a9f9f8_masa_admin;Password=ahmedadel2102023"
    ///     }
    ///
    ///     Required Fields: [label , type , url]
    ///
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
    /// First request:
    ///
    ///     {
    ///       "id": "16f29533-ab82-457a-85e8-e74e549e649e",
    ///       "label": "meili sync",
    ///       "user": "postgres",
    ///       "host": "localhost",
    ///       "port": 3000,
    ///       "database": "meilisync",
    ///       "type": 0,
    ///       "configurations": {
    ///         "sslmode": "prefer",
    ///         "timeout": "30"
    ///       }
    ///     }
    ///
    ///     Required Fields: [id, label , user , host , port , database , type]
    ///
    /// Second request:
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
}
