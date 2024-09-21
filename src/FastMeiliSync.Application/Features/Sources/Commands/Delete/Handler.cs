namespace FastMeiliSync.Application.Features.Sources.Commands.Delete;

internal class DeleteSourceByIdHandler : IRequestHandler<DeleteSourceByIdCommand, Response>
{
    readonly IMeiliSyncUnitOfWork _unitOfWork;

    public DeleteSourceByIdHandler(IMeiliSyncUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Response> Handle(
        DeleteSourceByIdCommand command,
        CancellationToken cancellationToken
    )
    {
        using (
            var tranasction = await _unitOfWork.BeginTransactionAsync(
                IsolationLevel.Snapshot,
                cancellationToken
            )
        )
        {
            var modifiedRows = 0;
            var source = await _unitOfWork.Sources.GetByIdAsync(
                command.Id,
                cancellationToken: cancellationToken
            );
            modifiedRows++;
            await _unitOfWork.Sources.DeleteAsync(source, cancellationToken);
            var success = await _unitOfWork.SaveChangesAsync(modifiedRows, cancellationToken);
            if (success)
            {
                await tranasction.CommitAsync(cancellationToken);
                return new Response
                {
                    Success = !default(bool),
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "operation done successfully"
                };
            }
            await tranasction.RollbackAsync(cancellationToken);
            throw new DatabaseTransactionException();
        }
    }
}
