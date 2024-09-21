using FastMeiliSync.Application.Features.Sources.Commands.Create;

public sealed class CreateSourceValidator : AbstractValidator<CreateSourceCommand>
{
    private readonly IServiceProvider _serviceProvider;

    public CreateSourceValidator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        var scope = _serviceProvider.CreateScope();
        ValidateRequest(scope.ServiceProvider.GetRequiredService<IMeiliSyncUnitOfWork>());
    }

    void ValidateRequest(IMeiliSyncUnitOfWork unitOfWork)
    {
        //  RuleFor(x => x.Database).NotEmpty().WithMessage().NotNull();
    }
}
