namespace FastMeiliSync.Application.Features.Sources.Commands.Update;

internal sealed record UpdateSourceHandler : IRequestHandler<UpdateSourceCommand, Response>
{
    readonly IMeiliSyncUnitOfWork _unitOfWork;

    public UpdateSourceHandler(IMeiliSyncUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Response> Handle(
        UpdateSourceCommand command,
        CancellationToken cancellationToken
    )
    {
        using (
            var transaction = await _unitOfWork.BeginTransactionAsync(
                IsolationLevel.Snapshot,
                cancellationToken
            )
        )
        {
            var modifiedRows = 0;
            Source source = await _unitOfWork.Sources.GetByIdAsync(
                command.Id,
                cancellationToken: cancellationToken
            );

            source.Update(
                command.Label,
                command.Host,
                command.Port,
                command.Database,
                command.Url,
                command.Type
            );

            modifiedRows++;
            var sourceEntry = await _unitOfWork.Sources.UpdateAsync(source, cancellationToken);
            var success = await _unitOfWork.SaveChangesAsync(modifiedRows, cancellationToken);
            if (success)
            {
                await transaction.CommitAsync(cancellationToken);
                return new ResponseOf<UpdateSourceResult>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Success = success,
                    Result = sourceEntry.Entity
                };
            }
            await transaction.RollbackAsync(cancellationToken);
            throw new DatabaseTransactionException();
        }
    }
}
