using FastMeiliSync.Domain.Entities.Tokens;
using FastMeiliSync.Shared.Context;

namespace FastMeiliSync.Application.Features.Users.Commands.Logout;

internal sealed record LogoutUserCommandHandle(
    IMeiliSyncUnitOfWork unitOfWork,
    IUserContext userContext
) : IRequestHandler<LogoutUserCommand, Response>
{
    public async Task<Response> Handle(
        LogoutUserCommand request,
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
            var userToken = await unitOfWork.Tokens.GetAsync<Token>(x =>
                x.UserId == userContext.UserId.Value
            );

            if (userToken is not null)
            {
                modifiedRows++;
                await unitOfWork.Tokens.DeleteAsync(userToken, cancellationToken);
            }

            var succes = await unitOfWork.SaveChangesAsync(modifiedRows, cancellationToken);
            if (succes)
            {
                await transaction.CommitAsync(cancellationToken);
                return new Response
                {
                    Success = true,
                    Message = ValidationMessages.Success,
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            await transaction.RollbackAsync(cancellationToken);
            throw new DatabaseTransactionException();
        }
    }
}
