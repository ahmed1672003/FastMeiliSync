namespace FastMeiliSync.Application.Features.Roles.Queries.GetById;

public sealed record GetUserByIdQueryHandler(IMeiliSyncUnitOfWork unitOfWork)
    : IRequestHandler<GetUserByIdQuery, Response>
{
    public async Task<Response> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        using (
            var transaction = await unitOfWork.BeginTransactionAsync(
                IsolationLevel.ReadCommitted,
                cancellationToken
            )
        )
        {
            var user = await unitOfWork.Users.GetByIdAsync(
                request.Id,
                u => u.Include(x => x.UserRoles).ThenInclude(x => x.Role),
                cancellationToken: cancellationToken
            );

            return new ResponseOf<GetUserByIdResult>
            {
                Message = ValidationMessages.Success,
                Success = true,
                StatusCode = (int)HttpStatusCode.OK,
                Result = user
            };
        }
    }
}
