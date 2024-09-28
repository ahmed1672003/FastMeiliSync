namespace FastMeiliSync.Application.Features.Roles.Commands.Update;

public sealed class UpdateRoleValidator : AbstractValidator<UpdateRoleCommand>
{
    private readonly IServiceProvider _serviceProvider;

    public UpdateRoleValidator(IServiceProvider serviceProvider)
    {
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
                        x.Id == req.Id
                        && x.Name.Trim().ToLower() == nameof(BasicRoles.Admin).ToLower()
                    );
                }
            )
            .WithMessage(x => ValidationMessages.Role.CanntDeleteAdminRole);

        RuleFor(x => x)
            .MustAsync(
                async (req, cancellationToken) =>
                {
                    return await unitOfWork.Roles.AnyAsync(x => x.Id == req.Id);
                }
            )
            .WithMessage(x => ValidationMessages.Role.NotFound);

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    return !await unitOfWork.Roles.AnyAsync(x =>
                        x.Name.Trim().ToLower() == req.Name.Trim().ToLower() && x.Id != req.Id
                    );
                }
            )
            .WithMessage(x => ValidationMessages.Role.NameExist);
    }
}
