namespace FastMeiliSync.Application.Features.MeiliSearches.Commands.Update;

public sealed class UdpateMeiliSaerchValidator : AbstractValidator<UpdateMeiliSearchCommand>
{
    private readonly IServiceProvider _serviceProvider;

    public UdpateMeiliSaerchValidator(IServiceProvider serviceProvider)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        ClassLevelCascadeMode = CascadeMode.Stop;

        _serviceProvider = serviceProvider;
        var scope = _serviceProvider.CreateScope();
        ValidateRequest(scope.ServiceProvider.GetRequiredService<IMeiliSyncUnitOfWork>());
    }

    void ValidateRequest(IMeiliSyncUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Label)
            .NotNull()
            .WithMessage(x => ValidationMessages.MeiliSearch.LabelRequired)
            .NotEmpty()
            .WithMessage(x => ValidationMessages.MeiliSearch.LabelRequired);

        RuleFor(x => x.ApiKey)
            .NotNull()
            .WithMessage(x => ValidationMessages.MeiliSearch.ApiKeyRequired)
            .NotEmpty()
            .WithMessage(x => ValidationMessages.MeiliSearch.ApiKeyRequired);

        RuleFor(x => x.Url)
            .NotNull()
            .WithMessage(x => ValidationMessages.MeiliSearch.UrlExist)
            .NotEmpty()
            .WithMessage(x => ValidationMessages.MeiliSearch.UrlExist);

        RuleFor(x => x)
            .MustAsync(
                async (req, cancellationToken) =>
                {
                    return await unitOfWork.MeiliSearches.AnyAsync(x => x.Id == req.Id);
                }
            )
            .WithMessage(x => ValidationMessages.MeiliSearch.NotFound);

        RuleFor(x => x)
            .MustAsync(
                async (req, cancellationToken) =>
                {
                    return !await unitOfWork.MeiliSearches.AnyAsync(x =>
                        x.Label.Trim().ToLower() == req.Label.Trim().ToLower() && x.Id != req.Id
                    );
                }
            )
            .WithMessage(x => ValidationMessages.MeiliSearch.LabelExist);
        RuleFor(x => x)
            .MustAsync(
                async (req, cancellationToken) =>
                {
                    return !await unitOfWork.MeiliSearches.AnyAsync(x =>
                        x.Url.Trim().ToLower() == req.Url.Trim().ToLower() && x.Id != req.Id
                    );
                }
            )
            .WithMessage(x => ValidationMessages.MeiliSearch.UrlExist);
    }
}
