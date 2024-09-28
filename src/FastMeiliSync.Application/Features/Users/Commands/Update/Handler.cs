namespace FastMeiliSync.Application.Features.Users.Commands.Update;

internal sealed record UpdateUserCommandHandler(IMeiliSyncUnitOfWork unitOfWork)
    : IRequestHandler<UpdateUserCommand, Response>
{
    public async Task<Response> Handle(
        UpdateUserCommand request,
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
                u => u.Include(x => x.UserRoles),
                stopTracking: false,
                cancellationToken: cancellationToken
            );

            user.UpdateProfile(request.Name, request.UserName, request.Email);

            if (user.UserRoles.Any())
            {
                modifiedRows += user.UserRoles.Count;
                await unitOfWork.UsersRoles.DeleteRangeAsync(user.UserRoles);
            }

            user.AssignToRoles(request.RoleIds);
            modifiedRows += user.UserRoles.Count;

            modifiedRows++;
            await unitOfWork.Users.UpdateAsync(user, cancellationToken);

            if (await unitOfWork.Tokens.AnyAsync(t => t.UserId == user.Id))
            {
                modifiedRows++;
                await unitOfWork.Tokens.DeactivateTokneAsync(user, cancellationToken);
            }

            var success = await unitOfWork.SaveChangesAsync(modifiedRows, cancellationToken);
            if (success)
            {
                await transaction.CommitAsync(cancellationToken);
                user = await unitOfWork.Users.GetByIdAsync(
                    request.Id,
                    u => u.Include(x => x.UserRoles).ThenInclude(x => x.Role),
                    cancellationToken: cancellationToken
                );
                return new ResponseOf<UpdateUserResult>
                {
                    Success = success,
                    Message = ValidationMessages.Success,
                    StatusCode = (int)HttpStatusCode.OK,
                    Result = user
                };
            }
            await transaction.RollbackAsync(cancellationToken);
            throw new DatabaseTransactionException();
        }
    }
}
