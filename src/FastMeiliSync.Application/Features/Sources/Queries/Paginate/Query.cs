using static FastMeiliSync.Application.Features.Sources.Queries.Paginate.PaginateSourceQuery;

namespace FastMeiliSync.Application.Features.Sources.Queries.Paginate;

public sealed record PaginateSourceQuery(
    int PageNumber = 1,
    int PageSize = 10,
    string Search = "",
    bool OrderBeforPagination = true,
    SourceOrderBy OrderBy = SourceOrderBy.CreatedOn,
    OrderByDirection OrderByDirection = OrderByDirection.Descending
) : IRequest<Response>
{
    public enum SourceOrderBy
    {
        Label,
        Url,
        CreatedOn
    }
}
