using FastMeiliSync.Application.Features.Roles.Queries.GetById;

namespace FastMeiliSync.Application.Features.Users.Commands.Seed;

internal sealed record SeedRolesCommandHandler(IMeiliSyncUnitOfWork unitOfWork)
    : IRequestHandler<SeedRolesCommand, Response>
{
    public async Task<Response> Handle(
        SeedRolesCommand request,
        CancellationToken cancellationToken
    )
    {
        using (
            var transaction = await unitOfWork.BeginTransactionAsync(
                IsolationLevel.Serializable,
                cancellationToken
            )
        )
        {
            var modifiedRows = 0;
            var role = Role.Create(nameof(BasicRoles.Admin));

            modifiedRows++;
            var roleEntry = await unitOfWork.Roles.CreateAsync(role, cancellationToken);

            var success = await unitOfWork.SaveChangesAsync(modifiedRows, cancellationToken);
            if (success)
            {
                await transaction.CommitAsync(cancellationToken);
                return new ResponseOf<GetRoleByIdResult>
                {
                    Success = success,
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = ValidationMessages.Success,
                    Result = roleEntry.Entity
                };
            }
            await transaction.RollbackAsync(cancellationToken);
            throw new DatabaseTransactionException();
        }
    }
}
