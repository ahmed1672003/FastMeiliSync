namespace FastMeiliSync.Application.Features.Sources.Queries.GetById;

public class GetSourceByIdValidator : AbstractValidator<GetSourceByIdQuery>
{
    private readonly IServiceProvider _serviceProvider;

    public GetSourceByIdValidator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        var scope = _serviceProvider.CreateScope();
        ValidateRequest(scope.ServiceProvider.GetRequiredService<IMeiliSyncUnitOfWork>());
    }

    void ValidateRequest(IMeiliSyncUnitOfWork unitOfWork)
    {
        RuleFor(x => x)
            .MustAsync(
                async (req, cancellationToken) =>
                {
                    return await unitOfWork.Sources.AnyAsync(x => x.Id == req.Id);
                }
            )
            .WithMessage(x => ValidationMessages.Source.NotFound);
    }
}
