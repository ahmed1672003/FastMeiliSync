namespace FastMeiliSync.Application.Features.Syncs.Queries.Paginate;

public sealed record PaginateUserResult(
    Guid Id,
    string Name,
    string UserName,
    string Email,
    DateTime CreatedOn,
    IEnumerable<GetRoleByIdResult> Roles
);
