using FastMeiliSync.Application.Abstractions;
using FastMeiliSync.Application.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace FastMeiliSync.Application.Features.Sources.Commands.Create;

internal sealed record CreateSourceCommandHandler(
    IMeiliSyncUnitOfWork unitOfWork,
    IHubContext<FastMeiliSyncHub, IFastMeiliSyncHubClient> hubContext
) : IRequestHandler<CreateSourceCommand, Response>
{
    public async Task<Response> Handle(
        CreateSourceCommand request,
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
            Source source = request;

            modifiedRows++;
            var sourceEntry = await unitOfWork.Sources.CreateAsync(source, cancellationToken);
            var success = await unitOfWork.SaveChangesAsync(modifiedRows, cancellationToken);
            if (success)
            {
                await transaction.CommitAsync(cancellationToken);

                var response = new ResponseOf<CreateSourceResult>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Success = success,
                    Result = sourceEntry.Entity,
                    Message = "operation done successfully"
                };
                await hubContext.Clients.All.NotifySourceAsync(
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
