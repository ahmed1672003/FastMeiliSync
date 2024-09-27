using Microsoft.AspNetCore.Identity;

namespace FastMeiliSync.Application.Features.Users.Commands.Create;

public sealed record CreateUserCommandHandler(
    IMeiliSyncUnitOfWork unitOfWork,
    IPasswordHasher<User> passwordHasher
) : IRequestHandler<CreateUserCommand, Response>
{
    public async Task<Response> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken
    )
    {
        using (
            var transaction = await unitOfWork.BeginTransactionAsync(
                cancellationToken: cancellationToken
            )
        )
        {
            var modifiedRows = 0;
            User user = request;
            user.HashPassword(passwordHasher, request.Password);

            modifiedRows += request.RoleIds.Count;
            user.AssignToRoles(request.RoleIds);

            modifiedRows++;
            var userEntry = await unitOfWork.Users.CreateAsync(user, cancellationToken);
            var success = await unitOfWork.SaveChangesAsync(modifiedRows, cancellationToken);

            if (success)
            {
                await transaction.CommitAsync(cancellationToken);

                user = await unitOfWork.Users.GetByIdAsync(
                    user.Id,
                    u => u.Include(x => x.UserRoles).ThenInclude(x => x.Role),
                    cancellationToken: cancellationToken
                );
                return new ResponseOf<CreateUserResult>
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
