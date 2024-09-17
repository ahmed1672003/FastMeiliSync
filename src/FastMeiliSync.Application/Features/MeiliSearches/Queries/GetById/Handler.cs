namespace FastMeiliSync.Application.Features.MeiliSearches.Queries.GetById;

internal sealed record GetMeiliSearchByIdHandler(IMeiliSyncUnitOfWork unitOfWork)
    : IRequestHandler<GetMeiliSearchByIdQuery, Response>
{
    public async Task<Response> Handle(
        GetMeiliSearchByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        using (
            var transaction = await unitOfWork.BeginTransactionAsync(
                IsolationLevel.ReadCommitted,
                cancellationToken
            )
        )
        {
            var meiliSearch = await unitOfWork.MeiliSearches.GetByIdAsync(
                request.Id,
                cancellationToken: cancellationToken
            );

            return new ResponseOf<GetMeiliSearchByIdResult>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Success = !default(bool),
                Result = meiliSearch
            };
        }
    }
}
