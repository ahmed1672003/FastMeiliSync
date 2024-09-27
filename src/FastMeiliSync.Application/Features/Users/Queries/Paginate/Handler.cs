using static FastMeiliSync.Application.Features.Syncs.Queries.Paginate.PaginateUserQuery;

namespace FastMeiliSync.Application.Features.Syncs.Queries.Paginate;

internal sealed record PaginateUsersHandler(IMeiliSyncUnitOfWork unitOfWork)
    : IRequestHandler<PaginateUserQuery, Response>
{
    public async Task<Response> Handle(
        PaginateUserQuery request,
        CancellationToken cancellationToken
    )
    {
        using (
            var transaction = await unitOfWork.BeginTransactionAsync(
                cancellationToken: cancellationToken
            )
        )
        {
            Expression<Func<User, object>> orderBy = request.OrderBy switch
            {
                UserOrderBy.Name => x => x.Name,
                UserOrderBy.UserName => u => u.Name,
                UserOrderBy.Email => u => u.Email,
                _ => m => m.CreatedOn,
            };

            var totalCount = await unitOfWork.Users.CountAsync(
                cancellationToken: cancellationToken
            );

            var items = await unitOfWork.Users.PaginateAsync(
                request.PageNumber,
                request.PageSize,
                orderBy,
                request.OrderByDirection,
                request.OrderBeforPagination,
                criteria: m =>
                    m.Name.Contains(request.Search)
                    || m.UserName.Contains(request.Search)
                    || m.Email.Contains(request.Search)
                    || m.CreatedOn.ToString().Contains(request.Search),
                selector: user => new PaginateUserResult(
                    user.Id,
                    user.Name,
                    user.UserName,
                    user.Email,
                    user.CreatedOn,
                    user.UserRoles.Select(x => x.Role)
                        .Select(x => new GetRoleByIdResult(x.Id, x.Name, x.CreatedOn))
                ),
                cancellationToken: cancellationToken
            );

            return new PagedResponseOf<PaginateUserResult>
            {
                Success = true,
                Message = "operation done successfully",
                StatusCode = (int)HttpStatusCode.OK,
                CurrentPage = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = totalCount,
                Items = items
            };
        }
    }
}
