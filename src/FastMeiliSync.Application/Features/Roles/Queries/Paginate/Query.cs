namespace FastMeiliSync.Application.Features.Roles.Queries.Paginate;

public sealed record PaginateRoleQuery(
    int PageNumber = 1,
    int PageSize = 10,
    string Search = "",
    bool OrderBeforPagination = true,
    RoleOrderBy OrderBy = RoleOrderBy.CreatedOn,
    OrderByDirection OrderByDirection = OrderByDirection.Descending
) : IRequest<Response>
{
    public enum RoleOrderBy
    {
        Name,
        CreatedOn
    }
}
