namespace FastMeiliSync.Application.Features.Sources.Queries.GetById;

internal sealed record GetSourceByIdQueryHandler : IRequestHandler<GetSourceByIdQuery, Response>
{
    readonly IMeiliSyncUnitOfWork _unitOfWork;

    public GetSourceByIdQueryHandler(IMeiliSyncUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Response> Handle(
        GetSourceByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        using (
            var transaction = await _unitOfWork.BeginTransactionAsync(
                IsolationLevel.ReadCommitted,
                cancellationToken
            )
        )
        {
            Source source = await _unitOfWork.Sources.GetByIdAsync(
                request.Id,
                stopTracking: false,
                cancellationToken: cancellationToken
            );
            await transaction.CommitAsync();
            return new ResponseOf<GetSourceByIdResult>
            {
                Result = source,
                StatusCode = (int)HttpStatusCode.OK,
                Success = !default(bool)
            };
        }
    }
}
