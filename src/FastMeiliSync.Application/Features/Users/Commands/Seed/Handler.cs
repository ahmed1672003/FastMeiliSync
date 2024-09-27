using FastMeiliSync.Application.Features.Roles.Queries.GetById;
using Microsoft.AspNetCore.Identity;

namespace FastMeiliSync.Application.Features.Users.Commands.Seed;

internal sealed record SeedUsersCommandHandler(
    IMeiliSyncUnitOfWork unitOfWork,
    IPasswordHasher<User> passwordHasher
) : IRequestHandler<SeedUsersCommand, Response>
{
    public async Task<Response> Handle(
        SeedUsersCommand request,
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

            var user = User.Create("admin", "meilisync", "info@meilisync.info");
            user.HashPassword(passwordHasher, "admin123");

            var roleId = await unitOfWork.Roles.GetAsync(
                r => r.Name.ToLower() == nameof(BasicRoles.Admin).ToLower(),
                r => r.Id,
                cancellationToken: cancellationToken
            );

            modifiedRows++;
            user.AssignToRoles(new() { roleId });

            modifiedRows++;
            var userEntry = await unitOfWork.Users.CreateAsync(user, cancellationToken);

            var success = await unitOfWork.SaveChangesAsync(modifiedRows, cancellationToken);
            if (success)
            {
                await transaction.CommitAsync(cancellationToken);
                user = await unitOfWork.Users.GetByIdAsync(
                    user.Id,
                    x => x.Include(x => x.UserRoles).ThenInclude(x => x.Role)
                );
                return new ResponseOf<GetUserByIdResult>
                {
                    Success = success,
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = ValidationMessages.Success,
                    Result = user
                };
            }
            await transaction.RollbackAsync(cancellationToken);
            throw new DatabaseTransactionException();
        }
    }
}
