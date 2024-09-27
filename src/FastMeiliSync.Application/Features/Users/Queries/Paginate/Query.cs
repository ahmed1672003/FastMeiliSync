using static FastMeiliSync.Application.Features.Syncs.Queries.Paginate.PaginateUserQuery;

namespace FastMeiliSync.Application.Features.Syncs.Queries.Paginate;

public sealed record PaginateUserQuery(
    int PageNumber = 1,
    int PageSize = 10,
    string Search = "",
    bool OrderBeforPagination = true,
    UserOrderBy OrderBy = UserOrderBy.CreatedOn,
    OrderByDirection OrderByDirection = OrderByDirection.Descending
) : IRequest<Response>
{
    public enum UserOrderBy
    {
        Name,
        Email,
        UserName,
        CreatedOn
    }
}
