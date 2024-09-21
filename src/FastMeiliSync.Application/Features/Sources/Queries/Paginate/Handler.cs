using System.Linq.Expressions;
using static FastMeiliSync.Application.Features.MeiliSearches.Queries.Paginate.PaginateSourceQuery;

namespace FastMeiliSync.Application.Features.MeiliSearches.Queries.Paginate;

internal sealed record PaginateSourceHandler(IMeiliSyncUnitOfWork unitOfWork)
    : IRequestHandler<PaginateSourceQuery, Response>
{
    public async Task<Response> Handle(
        PaginateSourceQuery request,
        CancellationToken cancellationToken
    )
    {
        using (
            var transaction = await unitOfWork.BeginTransactionAsync(
                cancellationToken: cancellationToken
            )
        )
        {
            Expression<Func<Source, object>> orderBy = request.OrderBy switch
            {
                SourceOrderBy.Label => s => s.Label,
                SourceOrderBy.Url => u => u.Url,
                _ => m => m.CreatedOn,
            };

            var totalCount = await unitOfWork.MeiliSearches.CountAsync(
                cancellationToken: cancellationToken
            );

            var items = await unitOfWork.Sources.PaginateAsync<PaginateSourceResult>(
                request.PageNumber,
                request.PageSize,
                orderBy,
                request.OrderByDirection,
                request.OrderBeforPagination,
                criteria: m =>
                    m.Label.Contains(request.Search)
                    || m.Url.Contains(request.Search)
                    || m.CreatedOn.ToString().Contains(request.Search),
                selector: source => new PaginateSourceResult(
                    source.Id,
                    source.Label,
                    source.Url,
                    source.CreatedOn
                ),
                cancellationToken: cancellationToken
            );

            return new PagedResponseOf<PaginateSourceResult>
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
