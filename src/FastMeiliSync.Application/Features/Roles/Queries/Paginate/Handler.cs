namespace FastMeiliSync.Application.Features.Roles.Queries.Paginate;

internal sealed record PaginateRoleHandler(IMeiliSyncUnitOfWork unitOfWork)
    : IRequestHandler<PaginateRoleQuery, Response>
{
    public async Task<Response> Handle(
        PaginateRoleQuery request,
        CancellationToken cancellationToken
    )
    {
        using (
            var transaction = await unitOfWork.BeginTransactionAsync(
                cancellationToken: cancellationToken
            )
        )
        {
            Expression<Func<Role, object>> orderBy = request.OrderBy switch
            {
                RoleOrderBy.Name => r => r.Name,
                _ => r => r.CreatedOn,
            };

            var totalCount = await unitOfWork.Roles.CountAsync(
                cancellationToken: cancellationToken
            );

            var items = await unitOfWork.Roles.PaginateAsync(
                request.PageNumber,
                request.PageSize,
                orderBy,
                request.OrderByDirection,
                request.OrderBeforPagination,
                criteria: m =>
                    m.Name.Contains(request.Search)
                    || m.CreatedOn.ToString().Contains(request.Search),
                selector: role => new PaginateRoleResult(role.Id, role.Name, role.CreatedOn),
                cancellationToken: cancellationToken
            );

            return new PagedResponseOf<PaginateRoleResult>
            {
                Success = true,
                Message = ValidationMessages.Success,
                StatusCode = (int)HttpStatusCode.OK,
                CurrentPage = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = totalCount,
                Items = items
            };
        }
    }
}
