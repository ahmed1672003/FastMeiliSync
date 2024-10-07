using FastMeiliSync.Application.Abstractions;
using FastMeiliSync.Application.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace FastMeiliSync.Application.Features.Syncs.Commands.Create;

internal sealed record CreateSyncCommandHandler(
    IMeiliSyncUnitOfWork unitOfWork,
    IHubContext<FastMeiliSyncHub, IFastMeiliSyncHubClient> hubContext
) : IRequestHandler<CreateSyncCommand, Response>
{
    public async Task<Response> Handle(
        CreateSyncCommand request,
        CancellationToken cancellationToken
    )
    {
        using (
            var transaction = await unitOfWork.BeginTransactionAsync(
                IsolationLevel.Serializable,
                cancellationToken
            )
        )
        {
            var modifiedRows = 0;
            Sync sync = request;

            modifiedRows++;
            var syncEntry = await unitOfWork.Syncs.CreateAsync(sync, cancellationToken);
            var success = await unitOfWork.SaveChangesAsync(modifiedRows, cancellationToken);
            if (success)
            {
                await transaction.CommitAsync(cancellationToken);
                await syncEntry.Reference(x => x.MeiliSearch).LoadAsync(cancellationToken);
                await syncEntry.Reference(x => x.Source).LoadAsync(cancellationToken);

                var response = new ResponseOf<CreateSyncResult>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Success = success,
                    Result = syncEntry.Entity,
                    Message = "operation done successfully"
                };
                await hubContext.Clients.All.NotifySyncAsync(
                    OperationType.Create,
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
