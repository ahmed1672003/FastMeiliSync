﻿using FastMeiliSync.Application.Abstractions;
using FastMeiliSync.Application.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace FastMeiliSync.Application.Features.MeiliSearches.Commands.Create;

internal sealed record CreateMeiliSaerchHandler(
    IMeiliSyncUnitOfWork unitOfWork,
    IHubContext<FastMeiliSyncHub, IFastMeiliSyncHubClient> hubContext
) : IRequestHandler<CreateMeiliSearchCommand, Response>
{
    public async Task<Response> Handle(
        CreateMeiliSearchCommand request,
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
            MeiliSearch meiliSearch = request;

            modifiedRows++;
            var meiliSearchEntry = await unitOfWork.MeiliSearches.CreateAsync(
                meiliSearch,
                cancellationToken
            );

            var success = await unitOfWork.SaveChangesAsync(modifiedRows, cancellationToken);

            if (success)
            {
                await transaction.CommitAsync(cancellationToken);
                var response = new ResponseOf<CreateMeiliSearchResult>
                {
                    Success = success,
                    Result = meiliSearchEntry.Entity,
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "operation done successfully"
                };
                await hubContext.Clients.All.NotifyMeiliSearchAsync(
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
