using FastMeiliSync.Application.Features.MeiliSearches.Queries.Paginate;

namespace FastMeiliSync.Application.Features.Syncs.Queries.Paginate;

public sealed class PaginateSyncValidator : AbstractValidator<PaginateMeiliSearchQuery>
{
    public PaginateSyncValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        ClassLevelCascadeMode = CascadeMode.Stop;
    }
}
