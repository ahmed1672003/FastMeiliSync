namespace FastMeiliSync.Application.Features.Roles.Commands.Delete;

internal sealed record DeleteRoleCommandHandler(IMeiliSyncUnitOfWork unitOfWork)
    : IRequestHandler<DeleteRoleCommand, Response>
{
    public async Task<Response> Handle(
        DeleteRoleCommand request,
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
            var role = await unitOfWork.Roles.GetByIdAsync(
                request.Id,
                cancellationToken: cancellationToken
            );

            modifiedRows++;
            await unitOfWork.Roles.DeleteAsync(role, cancellationToken);
            var success = await unitOfWork.SaveChangesAsync(modifiedRows, cancellationToken);
            if (success)
            {
                await transaction.CommitAsync(cancellationToken);
                return new Response
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Success = true,
                    Message = ValidationMessages.Success,
                };
            }
            await transaction.RollbackAsync(cancellationToken);
            throw new DatabaseTransactionException();
        }
    }
}
