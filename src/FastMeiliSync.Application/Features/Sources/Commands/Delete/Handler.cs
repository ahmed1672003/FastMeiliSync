namespace FastMeiliSync.Application.Features.Sources.Commands.Delete;

internal class DeleteSourceByIdHandler(IMeiliSyncUnitOfWork unitOfWork)
    : IRequestHandler<DeleteSourceByIdCommand, Response>
{
    public async Task<Response> Handle(
        DeleteSourceByIdCommand command,
        CancellationToken cancellationToken
    )
    {
        using (
            var tranasction = await unitOfWork.BeginTransactionAsync(
                IsolationLevel.Snapshot,
                cancellationToken
            )
        )
        {
            var modifiedRows = 0;
            var source = await unitOfWork.Sources.GetByIdAsync(
                command.Id,
                cancellationToken: cancellationToken
            );
            modifiedRows++;
            await unitOfWork.Sources.DeleteAsync(source, cancellationToken);
            var success = await unitOfWork.SaveChangesAsync(modifiedRows, cancellationToken);
            if (success)
            {
                await tranasction.CommitAsync(cancellationToken);

                return new Response
                {
                    Success = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "operation done successfully"
                };
            }
            await tranasction.RollbackAsync(cancellationToken);
            throw new DatabaseTransactionException();
        }
    }
}
