using System.Linq.Expressions;
using static FastMeiliSync.Application.Features.MeiliSearches.Queries.Paginate.PaginateMeiliSearchQuery;

namespace FastMeiliSync.Application.Features.MeiliSearches.Queries.Paginate;

internal sealed record PaginateMeiliSearchHandler(IMeiliSyncUnitOfWork unitOfWork)
    : IRequestHandler<PaginateMeiliSearchQuery, Response>
{
    public async Task<Response> Handle(
        PaginateMeiliSearchQuery request,
        CancellationToken cancellationToken
    )
    {
        using (
            var transaction = await unitOfWork.BeginTransactionAsync(
                cancellationToken: cancellationToken
            )
        )
        {
            Expression<Func<MeiliSearch, object>> orderBy = request.OrderBy switch
            {
                MeiliSearchOrderBy.Label => m => m.Label,
                MeiliSearchOrderBy.Url => m => m.Url,
                MeiliSearchOrderBy.ApiKey => m => m.ApiKey,
                _ => m => m.CreatedOn,
            };

            var totalCount = await unitOfWork.MeiliSearches.CountAsync(
                cancellationToken: cancellationToken
            );

            var items = await unitOfWork.MeiliSearches.PaginateAsync<PaginateMeiliSearchResult>(
                request.PageNumber,
                request.PageSize,
                orderBy,
                request.OrderByDirection,
                request.OrderBeforPagination,
                criteria: m =>
                    m.Label.Contains(request.Search)
                    || m.ApiKey.Contains(request.Search)
                    || m.Url.Contains(request.Search)
                    || m.CreatedOn.ToString().Contains(request.Search),
                selector: meiliSearch => new PaginateMeiliSearchResult(
                    meiliSearch.Id,
                    meiliSearch.Label,
                    meiliSearch.ApiKey,
                    meiliSearch.Url,
                    meiliSearch.CreatedOn
                ),
                cancellationToken: cancellationToken
            );

            return new PagedResponseOf<PaginateMeiliSearchResult>
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
