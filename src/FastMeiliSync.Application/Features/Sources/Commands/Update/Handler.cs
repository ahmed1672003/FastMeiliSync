using FastMeiliSync.Application.Abstractions;
using FastMeiliSync.Application.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace FastMeiliSync.Application.Features.Sources.Commands.Update;

public sealed record UpdateSourceHandler(
    IMeiliSyncUnitOfWork unitOfWork,
    IHubContext<FastMeiliSyncHub, IFastMeiliSyncHubClient> hubContext
) : IRequestHandler<UpdateSourceCommand, Response>
{
    public async Task<Response> Handle(
        UpdateSourceCommand command,
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
            Source source = await unitOfWork.Sources.GetByIdAsync(
                command.Id,
                cancellationToken: cancellationToken
            );

            source.Update(command.Label, command.Database, command.Url, command.Type);

            modifiedRows++;
            var sourceEntry = await unitOfWork.Sources.UpdateAsync(source, cancellationToken);
            var success = await unitOfWork.SaveChangesAsync(modifiedRows, cancellationToken);
            if (success)
            {
                await transaction.CommitAsync(cancellationToken);

                var response = new ResponseOf<UpdateSourceResult>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Success = success,
                    Result = sourceEntry.Entity,
                    Message = "operation done successfully"
                };

                await hubContext.Clients.All.NotifySourceAsync(
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
