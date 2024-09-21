namespace FastMeiliSync.Application.Features.MeiliSearches.Commands.Delete;

internal sealed record DeleteMeiliSearchByIdHandler(IMeiliSyncUnitOfWork unitOfWork)
    : IRequestHandler<DeleteMeiliSearchByIdCommand, Response>
{
    public async Task<Response> Handle(
        DeleteMeiliSearchByIdCommand request,
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
            var meiliSearch = await unitOfWork.MeiliSearches.GetByIdAsync(
                request.Id,
                cancellationToken: cancellationToken
            );

            modifiedRows++;
            await unitOfWork.MeiliSearches.DeleteAsync(meiliSearch);

            var success = await unitOfWork.SaveChangesAsync(modifiedRows, cancellationToken);
            if (success)
            {
                await transaction.CommitAsync(cancellationToken);
                return new Response
                {
                    Success = success,
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "operation done successfully"
                };
            }

            await transaction.RollbackAsync(cancellationToken);
            throw new DatabaseTransactionException();
        }
    }
}
