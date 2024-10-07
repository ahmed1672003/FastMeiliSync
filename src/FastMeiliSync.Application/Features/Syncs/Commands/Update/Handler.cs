using FastMeiliSync.Application.Abstractions;
using FastMeiliSync.Application.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace FastMeiliSync.Application.Features.Syncs.Commands.Update;

internal class UpdateSyncCommandHandler(
    IMeiliSyncUnitOfWork unitOfWork,
    IHubContext<FastMeiliSyncHub, IFastMeiliSyncHubClient> hubContext
) : IRequestHandler<UpdateSyncCommand, Response>
{
    public async Task<Response> Handle(
        UpdateSyncCommand request,
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

            sync.Update(request.Label, request.SourceId, request.MeiliSearchId);

            modifiedRows++;
            var syncEntry = await unitOfWork.Syncs.UpdateAsync(sync, cancellationToken);

            var success = await unitOfWork.SaveChangesAsync(modifiedRows, cancellationToken);
            if (success)
            {
                await transaction.CommitAsync(cancellationToken);
                await syncEntry.Reference(x => x.Source).LoadAsync();
                await syncEntry.Reference(x => x.MeiliSearch).LoadAsync();

                var response = new ResponseOf<UpdateSyncResult>
                {
                    Success = success,
                    Result = syncEntry.Entity,
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "operation done successfully"
                };

                await hubContext.Clients.All.NotifySyncAsync(
                    OperationType.Update,
                    response,
                    cancellationToken
                );

                return response;
            }

            await transaction.RollbackAsync(cancellationToken);
            throw new DatabaseTransactionException();
        }
    }
}
