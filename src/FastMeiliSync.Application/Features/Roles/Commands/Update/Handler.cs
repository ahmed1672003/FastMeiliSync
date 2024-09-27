namespace FastMeiliSync.Application.Features.Roles.Commands.Update;

internal sealed record UpdateRoleCommandHandler(IMeiliSyncUnitOfWork unitOfWork)
    : IRequestHandler<UpdateRoleCommand, Response>
{
    public async Task<Response> Handle(
        UpdateRoleCommand request,
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
            var role = await unitOfWork.Roles.GetByIdAsync(request.Id);

            role.Update(role.Name);

            modifiedRows++;
            var roleEntry = await unitOfWork.Roles.UpdateAsync(role, cancellationToken);
            var success = await unitOfWork.SaveChangesAsync(modifiedRows, cancellationToken);

            if (success)
            {
                await transaction.CommitAsync(cancellationToken);
                return new ResponseOf<UpdateRoleResult>
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
