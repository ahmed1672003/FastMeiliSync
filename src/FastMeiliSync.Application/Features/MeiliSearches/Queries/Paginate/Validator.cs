namespace FastMeiliSync.Application.Features.MeiliSearches.Queries.Paginate;

public sealed class PaginateMeiliSearchValidator : AbstractValidator<PaginateMeiliSearchQuery>
{
    public PaginateMeiliSearchValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        ClassLevelCascadeMode = CascadeMode.Stop;
    }
}
