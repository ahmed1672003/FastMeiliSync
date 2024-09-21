using FastMeiliSync.Shared.ValidationMessages;

namespace FastMeiliSync.Application.Features.MeiliSearches.Queries.GetById;

public sealed class GetMeiliSearchByIdValidator : AbstractValidator<GetMeiliSearchByIdQuery>
{
    private readonly IServiceProvider _serviceProvider;

    public GetMeiliSearchByIdValidator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        var scope = _serviceProvider.CreateScope();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IMeiliSyncUnitOfWork>();
        ValidateReques(unitOfWork);
    }

    void ValidateReques(IMeiliSyncUnitOfWork unitOfWork)
    {
        RuleFor(x => x)
            .MustAsync(
                async (req, cancellationToken) =>
                {
                    return await unitOfWork.MeiliSearches.AnyAsync(x => x.Id == req.Id);
                }
            )
            .WithMessage(x => ValidationMessages.MeiliSearch.InstanceNotFound);
    }
}
