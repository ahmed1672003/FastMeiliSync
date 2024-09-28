namespace FastMeiliSync.Application.Features.Users.Commands.Seed;

public sealed class SeedUsersValidator : AbstractValidator<SeedUsersCommand>
{
    private readonly IServiceProvider _serviceProvider;

    public SeedUsersValidator(IServiceProvider serviceProvider)
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
                    return !await unitOfWork.Users.AnyAsync(x => x.Master);
                }
            )
            .WithMessage(x => ValidationMessages.User.UserExist);

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    return await unitOfWork.Roles.AnyAsync(x =>
                        x.Name.Trim().ToLower() == nameof(BasicRoles.Admin).ToLower()
                    );
                }
            )
            .WithMessage(x => ValidationMessages.Role.AdminRoleNotFound);
    }
}
