using FastMeiliSync.Shared.Enums;
using static FastMeiliSync.Application.Features.MeiliSearches.Queries.Paginate.PaginateMeiliSearchQuery;

namespace FastMeiliSync.Application.Features.MeiliSearches.Queries.Paginate;

public sealed record PaginateMeiliSearchQuery(
    int PageNumber = 1,
    int PageSize = 10,
    string Search = "",
    bool OrderBeforPagination = true,
    MeiliSearchOrderBy OrderBy = MeiliSearchOrderBy.CreatedOn,
    OrderByDirection OrderByDirection = OrderByDirection.Ascending
) : IRequest<Response>
{
    public enum MeiliSearchOrderBy
    {
        Label,
        Url,
        ApiKey,
        CreatedOn
    }
}
