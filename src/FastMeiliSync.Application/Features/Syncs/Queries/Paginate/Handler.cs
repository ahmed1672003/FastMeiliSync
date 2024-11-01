using static FastMeiliSync.Application.Features.Syncs.Queries.Paginate.PaginateSyncQuery;

namespace FastMeiliSync.Application.Features.Syncs.Queries.Paginate;

internal sealed record PaginateSyncHandler(IMeiliSyncUnitOfWork unitOfWork)
    : IRequestHandler<PaginateSyncQuery, Response>
{
    public async Task<Response> Handle(
        PaginateSyncQuery request,
        CancellationToken cancellationToken
    )
    {
        using (
            var transaction = await unitOfWork.BeginTransactionAsync(
                cancellationToken: cancellationToken
            )
        )
        {
            Expression<Func<Sync, object>> orderBy = request.OrderBy switch
            {
                SyncOrderBy.Label => s => s.Label,
                _ => s => s.CreatedOn,
            };

            var totalCount = await unitOfWork.Syncs.CountAsync(
                cancellationToken: cancellationToken
            );

            var items = await unitOfWork.Syncs.PaginateAsync(
                request.PageNumber,
                request.PageSize,
                orderBy,
                request.OrderByDirection,
                request.OrderBeforPagination,
                criteria: s =>
                    s.Label.Contains(request.Search)
                    || s.CreatedOn.ToString().Contains(request.Search),
                includes: s => s.Include(x => x.Source).Include(x => x.MeiliSearch),
                selector: sync => new PaginateSyncResult(
                    sync.Id,
                    sync.Label,
                    sync.CreatedOn,
                    sync.MeiliSearch,
                    sync.Source
                ),
                cancellationToken: cancellationToken
            );

            return new PagedResponseOf<PaginateSyncResult>
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
