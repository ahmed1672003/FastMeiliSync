namespace FastMeiliSync.Application.Features.MeiliSearches.Commands.Create;

public sealed class CreateMeiliSearchValidator : AbstractValidator<CreateMeiliSearchCommand>
{
    private readonly IServiceProvider _serviceProvider;

    public CreateMeiliSearchValidator(IServiceProvider serviceProvider)
    {
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
                    return !await unitOfWork.MeiliSearches.AnyAsync(x =>
                        x.Label.Trim().ToLower() == req.Label.Trim().ToLower()
                    );
                }
            )
            .WithMessage(x => ValidationMessages.MeiliSearch.LabelExist);
        RuleFor(x => x)
            .MustAsync(
                async (req, cancellationToken) =>
                {
                    return !await unitOfWork.MeiliSearches.AnyAsync(x =>
                        x.Url.Trim().ToLower() == req.Url.Trim().ToLower()
                    );
                }
            )
            .WithMessage(x => ValidationMessages.MeiliSearch.UrlExist);
    }
}
