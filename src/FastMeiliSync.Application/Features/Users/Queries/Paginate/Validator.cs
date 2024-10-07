namespace FastMeiliSync.Application.Features.Syncs.Queries.Paginate;

public sealed class PaginateUserValidator : AbstractValidator<PaginateUserQuery>
{
    public PaginateUserValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        ClassLevelCascadeMode = CascadeMode.Stop;
    }
}
