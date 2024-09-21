using FastMeiliSync.Shared.Enums;
using static FastMeiliSync.Application.Features.MeiliSearches.Queries.Paginate.PaginateSourceQuery;

namespace FastMeiliSync.Application.Features.MeiliSearches.Queries.Paginate;

public sealed record PaginateSourceQuery(
    int PageNumber = 1,
    int PageSize = 10,
    string Search = "",
    bool OrderBeforPagination = true,
    SourceOrderBy OrderBy = SourceOrderBy.CreatedOn,
    OrderByDirection OrderByDirection = OrderByDirection.Ascending
) : IRequest<Response>
{
    public enum SourceOrderBy
    {
        Label,
        Url,
        CreatedOn
    }
}
