namespace FastMeiliSync.Application.Features.Users.Commands.Create;

public sealed record LogInUserCommandHandler(
    IMeiliSyncUnitOfWork unitOfWork,
    IJWTManager jWTManager
) : IRequestHandler<LogInUserCommand, Response>
{
    public async Task<Response> Handle(
        LogInUserCommand request,
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

            var user = await unitOfWork.Users.GetByEmailAsync(
                request.Email,
                u => u.Include(x => x.Token).Include(x => x.UserRoles).ThenInclude(x => x.Role),
                cancellationToken
            );

            var token = await jWTManager.GenerateTokenAsync(user, cancellationToken);

            modifiedRows++;
            if (user.Token != null)
                user.ChangeToken(token);
            else
                user.AddToken(token);

            modifiedRows++;
            if (user.UserRoles.Any())
            {
                modifiedRows += user.UserRoles.Count * 2;
            }

            await unitOfWork.Users.UpdateAsync(user, cancellationToken);

            var success = await unitOfWork.SaveChangesAsync(modifiedRows, cancellationToken);
            if (success)
            {
                await transaction.CommitAsync(cancellationToken);
                return new ResponseOf<LogInUserResult>
                {
                    Success = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = ValidationMessages.Success,
                    Result = new(user.Id, token)
                };
            }
            await transaction.RollbackAsync(cancellationToken);
            throw new DatabaseTransactionException();
        }
    }
}
