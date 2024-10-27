using FastMeiliSync.Application.Features.Syncs.Queries.GetById;

namespace FastMeiliSync.Application.Features.Syncs.Commands.Stop;

internal sealed record StopSyncHandler(IMeiliSyncUnitOfWork unitOfWork)
    : IRequestHandler<StopSyncCommand, Response>
{
    public async Task<Response> Handle(StopSyncCommand request, CancellationToken cancellationToken)
    {
        using (
            var transaction = await unitOfWork.BeginTransactionAsync(
                IsolationLevel.Snapshot,
                cancellationToken
            )
        )
        {
            var modifiedRows = 0;

            var sync = await unitOfWork.Syncs.GetByIdAsync(
                request.Id,
                stopTracking: false,
                cancellationToken: cancellationToken
            );

            sync.Stop();

            modifiedRows++;
            var syncEntry = await unitOfWork.Syncs.UpdateAsync(sync, cancellationToken);

            var success = await unitOfWork.SaveChangesAsync(modifiedRows, cancellationToken);
            if (success)
            {
                await transaction.CommitAsync(cancellationToken);
                await syncEntry.Reference(x => x.Source).LoadAsync(cancellationToken);
                await syncEntry.Reference(x => x.MeiliSearch).LoadAsync(cancellationToken);

                return new ResponseOf<GetSyncByIdResult>
                {
                    Message = ValidationMessages.Success,
                    Success = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Result = sync
                };
            }
            await transaction.RollbackAsync(cancellationToken);
            throw new DatabaseTransactionException();
        }
    }
}
