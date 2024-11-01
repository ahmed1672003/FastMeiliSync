﻿namespace FastMeiliSync.Application.Features.Syncs.Commands.Stop;

public sealed class StopSyncValidator : AbstractValidator<StopSyncCommand>
{
    private readonly IServiceProvider _serviceProvider;

    public StopSyncValidator(IServiceProvider serviceProvider)
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

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    return await unitOfWork.Syncs.AnyAsync(x => x.Id == req.Id && x.Working);
                }
            )
            .WithMessage(x => ValidationMessages.Sync.SyncAlreadyStopped);
    }
}