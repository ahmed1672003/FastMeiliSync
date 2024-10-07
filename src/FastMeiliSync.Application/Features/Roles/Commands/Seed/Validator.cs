namespace FastMeiliSync.Application.Features.Users.Commands.Seed;

public sealed class SeedRolesValidator : AbstractValidator<SeedRolesCommand>
{
    private readonly IServiceProvider _serviceProvider;

    public SeedRolesValidator(IServiceProvider serviceProvider)
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
                    return !await unitOfWork.Roles.AnyAsync(x =>
                        x.Name.Trim().ToLower() == nameof(BasicRoles.Admin).ToLower()
                    );
                }
            )
            .WithMessage(x => ValidationMessages.Role.RoleExist);
    }
}
