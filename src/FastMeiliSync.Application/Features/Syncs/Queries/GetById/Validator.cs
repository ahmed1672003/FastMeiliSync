namespace FastMeiliSync.Application.Features.Syncs.Queries.GetById;

public sealed class GetSyncByIdValidator : AbstractValidator<GetSyncByIdQuery>
{
    private readonly IServiceProvider _serviceProvider;

    public GetSyncByIdValidator(IServiceProvider serviceProvider)
    {
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
