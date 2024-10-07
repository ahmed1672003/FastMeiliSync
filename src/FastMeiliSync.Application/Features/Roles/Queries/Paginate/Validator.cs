namespace FastMeiliSync.Application.Features.Roles.Queries.Paginate;

public sealed class PaginateRoleValidator : AbstractValidator<PaginateRoleQuery>
{
    public PaginateRoleValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        ClassLevelCascadeMode = CascadeMode.Stop;
    }
}
