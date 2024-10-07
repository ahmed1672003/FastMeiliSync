namespace FastMeiliSync.Application.Features.Syncs.Commands.Delete;

public sealed class DeleteSyncByIdValidator : AbstractValidator<DeleteSyncByIdCommand>
{
    private readonly IServiceProvider _serviceProvider;

    public DeleteSyncByIdValidator(IServiceProvider serviceProvider)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        ClassLevelCascadeMode = CascadeMode.Stop;

        _serviceProvider = serviceProvider;
        var scope = _serviceProvider.CreateScope();
        ValidateRequest(scope.ServiceProvider.GetRequiredService<IMeiliSyncUnitOfWork>());
    }

    void ValidateRequest(IMeiliSyncUnitOfWork unitOfWork)
    {
        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    return await unitOfWork.Syncs.AnyAsync(x => x.Id == req.Id);
                }
            )
            .WithMessage(x => ValidationMessages.Sync.NotFound);
    }
}
