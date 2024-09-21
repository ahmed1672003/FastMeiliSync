using FastMeiliSync.Shared.ValidationMessages;

namespace FastMeiliSync.Application.Features.MeiliSearches.Commands.Create;

public sealed class CreateMeiliSearchValidator : AbstractValidator<CreateMeiliSearchCommand>
{
    private readonly IServiceProvider _serviceProvider;

    public CreateMeiliSearchValidator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        var scope = _serviceProvider.CreateScope();
        ValidateReques(scope.ServiceProvider.GetRequiredService<IMeiliSyncUnitOfWork>());
    }

    void ValidateReques(IMeiliSyncUnitOfWork unitOfWork)
    {
        RuleFor(x => x)
            .MustAsync(
                async (req, cancellationToken) =>
                {
                    return !await unitOfWork.MeiliSearches.AnyAsync(x =>
                        x.Label.Trim().ToLower() == req.Label.Trim().ToLower()
                    );
                }
            )
            .WithMessage(x => ValidationMessages.MeiliSearch.InstanceLabelExist);
        RuleFor(x => x)
            .MustAsync(
                async (req, cancellationToken) =>
                {
                    return !await unitOfWork.MeiliSearches.AnyAsync(x =>
                        x.Url.Trim().ToLower() == req.Url.Trim().ToLower()
                    );
                }
            )
            .WithMessage(x => ValidationMessages.MeiliSearch.InstanceUrlExist);
    }
}
