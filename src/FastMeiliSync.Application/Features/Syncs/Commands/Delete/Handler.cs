namespace FastMeiliSync.Application.Features.Syncs.Commands.Delete;

internal sealed record DeleteSyncByIdHandler(IMeiliSyncUnitOfWork unitOfWork)
    : IRequestHandler<DeleteSyncByIdCommand, Response>
{
    public async Task<Response> Handle(
        DeleteSyncByIdCommand request,
        CancellationToken cancellationToken
    )
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
                cancellationToken: cancellationToken
            );

            modifiedRows++;
            await unitOfWork.Syncs.DeleteAsync(sync, cancellationToken);
            var success = await unitOfWork.SaveChangesAsync(modifiedRows, cancellationToken);
            if (success)
            {
                await transaction.CommitAsync(cancellationToken);

                return new Response
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Success = success,
                    Message = "operation done successfully"
                };
            }
            await transaction.RollbackAsync(cancellationToken);
            throw new DatabaseTransactionException();
        }
    }
}
