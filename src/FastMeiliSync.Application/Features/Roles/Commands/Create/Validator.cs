namespace FastMeiliSync.Application.Features.Roles.Commands.Create;

public sealed class CreateRoleValidator : AbstractValidator<CreateRoleCommand>
{
    private readonly IServiceProvider _serviceProvider;

    public CreateRoleValidator(IServiceProvider serviceProvider)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        ClassLevelCascadeMode = CascadeMode.Stop;

        _serviceProvider = serviceProvider;
        var scope = _serviceProvider.CreateScope();
        ValidateRequest(scope.ServiceProvider.GetRequiredService<IMeiliSyncUnitOfWork>());
    }

    void ValidateRequest(IMeiliSyncUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Name)
            .NotNull()
            .WithMessage(x => ValidationMessages.Role.NameRequired)
            .NotEmpty()
            .WithMessage(x => ValidationMessages.Role.NameRequired);

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    return !await unitOfWork.Roles.AnyAsync(x =>
                        x.Name.Trim().ToLower() == req.Name.Trim().ToLower()
                    );
                }
            )
            .WithMessage(x => ValidationMessages.Role.NameExist);
    }
}
