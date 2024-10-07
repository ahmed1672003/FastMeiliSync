using FastMeiliSync.Application.Abstractions;
using FastMeiliSync.Application.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace FastMeiliSync.Application.Features.Syncs.Commands.Delete;

internal sealed record DeleteSyncByIdHandler(
    IMeiliSyncUnitOfWork unitOfWork,
    IHubContext<FastMeiliSyncHub, IFastMeiliSyncHubClient> hubContext
) : IRequestHandler<DeleteSyncByIdCommand, Response>
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

                var response = new ResponseOf<Guid>()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Success = success,
                    Message = "operation done successfully",
                    Result = sync.Id
                };
                await hubContext.Clients.All.NotifySyncAsync(
                    OperationType.Delete,
                    response,
                    cancellationToken
                );
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
