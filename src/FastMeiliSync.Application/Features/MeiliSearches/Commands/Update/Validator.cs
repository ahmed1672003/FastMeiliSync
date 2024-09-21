using FastMeiliSync.Shared.ValidationMessages;

namespace FastMeiliSync.Application.Features.MeiliSearches.Commands.Update;

public sealed class UdpateMeiliSaerchValidator : AbstractValidator<UpdateMeiliSearchCommand>
{
    private readonly IServiceProvider _serviceProvider;

    public UdpateMeiliSaerchValidator(IServiceProvider serviceProvider)
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

        RuleFor(x => x)
            .MustAsync(
                async (req, cancellationToken) =>
                {
                    return !await unitOfWork.MeiliSearches.AnyAsync(x =>
                        x.Label.Trim().ToLower() == req.Label.Trim().ToLower() && x.Id != req.Id
                    );
                }
            )
            .WithMessage(x => ValidationMessages.MeiliSearch.InstanceLabelExist);
        RuleFor(x => x)
            .MustAsync(
                async (req, cancellationToken) =>
                {
                    return !await unitOfWork.MeiliSearches.AnyAsync(x =>
                        x.Url.Trim().ToLower() == req.Url.Trim().ToLower() && x.Id != req.Id
                    );
                }
            )
            .WithMessage(x => ValidationMessages.MeiliSearch.InstanceUrlExist);
    }
}
