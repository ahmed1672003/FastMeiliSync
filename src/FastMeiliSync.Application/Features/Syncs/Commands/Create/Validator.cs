namespace FastMeiliSync.Application.Features.Syncs.Commands.Create;

public sealed class CreateSyncValidator : AbstractValidator<CreateSyncCommand>
{
    private readonly IServiceProvider _serviceProvider;

    public CreateSyncValidator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        var scope = _serviceProvider.CreateScope();
        ValidateRequest(scope.ServiceProvider.GetRequiredService<IMeiliSyncUnitOfWork>());
    }

    void ValidateRequest(IMeiliSyncUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Label)
            .NotNull()
            .WithMessage(x => ValidationMessages.Source.LabelRequired)
            .NotEmpty()
            .WithMessage(x => ValidationMessages.Source.LabelRequired);

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    return await unitOfWork.MeiliSearches.AnyAsync(x => x.Id == req.MeiliSearchId);
                }
            )
            .WithMessage(x => ValidationMessages.MeiliSearch.NotFound);

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    return await unitOfWork.Sources.AnyAsync(x => x.Id == req.SourceId);
                }
            )
            .WithMessage(x => ValidationMessages.Source.NotFound);

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    return !await unitOfWork.Syncs.AnyAsync(x =>
                        x.Label.Trim().ToLower() == req.Label.Trim().ToLower()
                    );
                }
            )
            .WithMessage(x => ValidationMessages.Sync.LabelExist);

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    return !await unitOfWork.Syncs.AnyAsync(x =>
                        x.MeiliSearchId == req.MeiliSearchId && x.SourceId == req.SourceId
                    );
                }
            )
            .WithMessage(x => ValidationMessages.Sync.SyncExist);
    }
}
