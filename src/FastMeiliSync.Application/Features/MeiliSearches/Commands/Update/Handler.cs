namespace FastMeiliSync.Application.Features.MeiliSearches.Commands.Update;

internal class UpdateMeiliSearchHandler(IMeiliSyncUnitOfWork unitOfWork)
    : IRequestHandler<UpdateMeiliSearchCommand, Response>
{
    public async Task<Response> Handle(
        UpdateMeiliSearchCommand request,
        CancellationToken cancellationToken
    )
    {
        using (
            var tranasction = await unitOfWork.BeginTransactionAsync(
                IsolationLevel.Serializable,
                cancellationToken
            )
        )
        {
            var modifiedRows = 0;

            var meiliSearch = await unitOfWork.MeiliSearches.GetByIdAsync(
                request.Id,
                cancellationToken: cancellationToken
            );

            meiliSearch.Update(request.Label, request.Url, request.ApiKey);

            modifiedRows++;
            var meiliSearchEntry = await unitOfWork.MeiliSearches.UpdateAsync(
                meiliSearch,
                cancellationToken
            );

            var success = await unitOfWork.SaveChangesAsync(modifiedRows, cancellationToken);
            if (success)
            {
                await tranasction.CommitAsync(cancellationToken);
                var response = new ResponseOf<UpdateMeiliSearchResult>
                {
                    Success = success,
                    Result = meiliSearchEntry.Entity,
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "operation done successfully"
                };

                return response;
            }
            await tranasction.RollbackAsync(cancellationToken);
            throw new DatabaseTransactionException();
        }
    }
}
