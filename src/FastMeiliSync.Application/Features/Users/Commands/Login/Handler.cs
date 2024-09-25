using System.Security.Claims;
using FastMeiliSync.Application.Features.Users.Commands.Login;

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

            if (user.Token != null)
            {
                modifiedRows++;
                await unitOfWork.Tokens.DeleteAsync(user.Token, cancellationToken);
            }

            var token = await jWTManager.GenerateTokenAsync(
                user,
                u =>
                    u.UserRoles.Select(x => new Claim(nameof(CustomClaimTypes.Roles), x.Role.Name))
                        .ToList(),
                cancellationToken
            );

            modifiedRows++;
            user.AddToken(token);

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
            await transaction.CommitAsync(cancellationToken);
            throw new DatabaseTransactionException();
        }
    }
}
