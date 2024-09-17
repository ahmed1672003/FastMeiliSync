namespace FastMeiliSync.Application.Features.Syncs.Queries.GetById;

internal sealed record GetSyncByIdHandler(IMeiliSyncUnitOfWork unitOfWork)
    : IRequestHandler<GetSyncByIdQuery, Response>
{
    public async Task<Response> Handle(
        GetSyncByIdQuery request,
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
            var sync = await unitOfWork.Syncs.GetByIdAsync(
                request.Id,
                x => x.Include(s => s.MeiliSearch).Include(s => s.Source),
                cancellationToken: cancellationToken
            );

            return new ResponseOf<GetSyncByIdResult>
            {
                Success = !default(bool),
                StatusCode = (int)HttpStatusCode.OK,
                Result = sync
            };
        }
    }
}
