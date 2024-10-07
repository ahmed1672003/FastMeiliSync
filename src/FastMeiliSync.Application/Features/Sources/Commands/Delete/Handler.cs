using FastMeiliSync.Application.Abstractions;
using FastMeiliSync.Application.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace FastMeiliSync.Application.Features.Sources.Commands.Delete;

internal class DeleteSourceByIdHandler(
    IMeiliSyncUnitOfWork unitOfWork,
    IHubContext<FastMeiliSyncHub, IFastMeiliSyncHubClient> hubContext
) : IRequestHandler<DeleteSourceByIdCommand, Response>
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

                var response = new ResponseOf<Guid>
                {
                    Success = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "operation done successfully",
                    Result = source.Id
                };

                await hubContext.Clients.All.NotifySourceAsync(
                    OperationType.Delete,
                    response,
                    cancellationToken
                );

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
