namespace FastMeiliSync.Application.Features.Sources.Commands.Create;

internal sealed record CreateSourceCommandHandler : IRequestHandler<CreateSourceCommand, Response>
{
    readonly IMeiliSyncUnitOfWork _unitOfWork;

    public CreateSourceCommandHandler(IMeiliSyncUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Response> Handle(
        CreateSourceCommand request,
        CancellationToken cancellationToken
    )
    {
        using (
            var transaction = await _unitOfWork.BeginTransactionAsync(
                IsolationLevel.Serializable,
                cancellationToken
            )
        )
        {
            var modifiedRows = 0;
            Source source = request;

            modifiedRows++;
            var sourceEntry = await _unitOfWork.Sources.CreateAsync(source, cancellationToken);
            var success = await _unitOfWork.SaveChangesAsync(modifiedRows, cancellationToken);
            if (success)
            {
                await transaction.CommitAsync(cancellationToken);
                return new ResponseOf<CreateSourceResult>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Success = success,
                    Result = sourceEntry.Entity,
                    Message = "operation done successfully"
                };
            }
            await transaction.RollbackAsync(cancellationToken);
            throw new DatabaseTransactionException();
        }
    }
}
