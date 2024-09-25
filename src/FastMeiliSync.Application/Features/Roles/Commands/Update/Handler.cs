using FastMeiliSync.Application.Features.Roles.Commands.Create;

namespace FastMeiliSync.Application.Features.Roles.Commands.Update;

internal sealed record UpdateRoleCommandHandler(IMeiliSyncUnitOfWork unitOfWork)
    : IRequestHandler<CreateRoleCommand, Response>
{
    public async Task<Response> Handle(
        CreateRoleCommand request,
        CancellationToken cancellationToken
    )
    {
        using (
            var transaction = await unitOfWork.BeginTransactionAsync(
                IsolationLevel.Snapshot,
                cancellationToken: cancellationToken
            )
        )
        {
            var modifiedRows = 0;
            Role role = request;

            modifiedRows++;
            var roleEntry = await unitOfWork.Roles.UpdateAsync(role, cancellationToken);
            var success = await unitOfWork.SaveChangesAsync(modifiedRows, cancellationToken);

            if (success)
            {
                await transaction.CommitAsync(cancellationToken);
                return new ResponseOf<CreateRoleResult>
                {
                    Success = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = ValidationMessages.Success,
                    Result = roleEntry.Entity,
                };
            }
            await transaction.RollbackAsync(cancellationToken);
            throw new DatabaseTransactionException();
        }
    }
}
