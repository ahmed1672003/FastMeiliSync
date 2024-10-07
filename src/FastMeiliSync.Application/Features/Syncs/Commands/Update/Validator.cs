namespace FastMeiliSync.Application.Features.Syncs.Commands.Update;

public class UpdateSyncValidator : AbstractValidator<UpdateSyncCommand>
{
    private readonly IServiceProvider _serviceProvider;

    public UpdateSyncValidator(IServiceProvider serviceProvider)
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
            .WithMessage(x => ValidationMessages.Source.LabelRequired)
            .NotEmpty()
            .WithMessage(x => ValidationMessages.Source.LabelRequired);

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    return await unitOfWork.Syncs.AnyAsync(x => x.Id == req.Id);
                }
            )
            .WithMessage(x => ValidationMessages.Sync.NotFound);

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    return !await unitOfWork.Syncs.AnyAsync(x =>
                        x.Label.Trim().ToLower() == req.Label.Trim().ToLower() && x.Id != req.Id
                    );
                }
            )
            .WithMessage(x => ValidationMessages.Sync.NotFound);

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
                        x.MeiliSearchId == req.MeiliSearchId
                        && x.SourceId == req.SourceId
                        && x.Id != req.Id
                    );
                }
            )
            .WithMessage(x => ValidationMessages.Sync.SyncExist);
    }
}
