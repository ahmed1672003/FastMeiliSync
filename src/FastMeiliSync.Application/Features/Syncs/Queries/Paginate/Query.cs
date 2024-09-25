using FastMeiliSync.Shared.Enums;
using static FastMeiliSync.Application.Features.Syncs.Queries.Paginate.PaginateSyncQuery;

namespace FastMeiliSync.Application.Features.Syncs.Queries.Paginate;

public sealed record PaginateSyncQuery(
    int PageNumber = 1,
    int PageSize = 10,
    string Search = "",
    bool OrderBeforPagination = true,
    SyncOrderBy OrderBy = SyncOrderBy.CreatedOn,
    OrderByDirection OrderByDirection = OrderByDirection.Ascending
) : IRequest<Response>
{
    public enum SyncOrderBy
    {
        Label,
        CreatedOn
    }
}
