namespace FastMeiliSync.Application.Features.Users.Commands.Delete;

public sealed record DeleteUserCommandHandler(IMeiliSyncUnitOfWork unitOfWork)
    : IRequestHandler<DeleteUserCommand, Response>
{
    public async Task<Response> Handle(
        DeleteUserCommand request,
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

            var user = await unitOfWork.Users.GetByIdAsync(
                request.Id,
                u => u.Include(x => x.Token).Include(x => x.UserRoles)
            );

            if (user.Token != null)
                modifiedRows++;

            modifiedRows += user.UserRoles.Count;

            modifiedRows++;
            await unitOfWork.Users.DeleteAsync(user, cancellationToken);

            var success = await unitOfWork.SaveChangesAsync(modifiedRows, cancellationToken);
            if (success)
            {
                await transaction.RollbackAsync(cancellationToken);
                return new Response
                {
                    Success = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = ValidationMessages.Success
                };
            }

            await transaction.RollbackAsync(cancellationToken);
            throw new DatabaseTransactionException();
        }
    }
}
