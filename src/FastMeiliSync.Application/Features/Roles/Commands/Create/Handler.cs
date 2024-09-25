namespace FastMeiliSync.Application.Features.Roles.Commands.Create;

internal sealed record CreateRoleCommandHandler(IMeiliSyncUnitOfWork unitOfWork)
    : IRequestHandler<CreateRoleCommand, Response>
{
    public async Task<Response> Handle(
        CreateRoleCommand request,
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
            Role role = request;

            modifiedRows++;
            var roleEntry = await unitOfWork.Roles.CreateAsync(role, cancellationToken);
            var success = await unitOfWork.SaveChangesAsync(modifiedRows, cancellationToken);

            if (success)
            {
                await transaction.CommitAsync(cancellationToken);
                return new ResponseOf<CreateRoleResult>
                {
                    Message = ValidationMessages.Success,
                    StatusCode = (int)HttpStatusCode.OK,
                    Success = true,
                    Result = roleEntry.Entity
                };
            }
            await transaction.RollbackAsync(cancellationToken);
            throw new DatabaseTransactionException();
        }
    }
}
