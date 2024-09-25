namespace FastMeiliSync.Application.Features.Roles.Queries.GetById;

public sealed record GetRoleByIdQueryHandler(IMeiliSyncUnitOfWork unitOfWork)
    : IRequestHandler<GetRoleByIdQuery, Response>
{
    public async Task<Response> Handle(
        GetRoleByIdQuery request,
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
            var role = await unitOfWork.Roles.GetByIdAsync(request.Id);
            return new ResponseOf<GetRoleByIdResult>
            {
                Message = ValidationMessages.Success,
                Success = true,
                StatusCode = (int)HttpStatusCode.OK,
                Result = role
            };
        }
    }
}
