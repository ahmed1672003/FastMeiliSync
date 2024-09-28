namespace FastMeiliSync.Application.Features.Users.Commands.Create;

public sealed class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    private readonly IServiceProvider _serviceProvider;

    public CreateUserValidator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        var scope = _serviceProvider.CreateScope();
        ValidateRequest(scope.ServiceProvider.GetRequiredService<IMeiliSyncUnitOfWork>());
    }

    void ValidateRequest(IMeiliSyncUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Name)
            .NotNull()
            .WithMessage(x => ValidationMessages.User.UserNameRequired)
            .NotEmpty()
            .WithMessage(x => ValidationMessages.User.UserNameRequired);

        RuleFor(x => x.UserName)
            .NotNull()
            .WithMessage(x => ValidationMessages.User.UserNameRequired)
            .NotEmpty()
            .WithMessage(x => ValidationMessages.User.UserNameRequired);

        RuleFor(x => x.Email)
            .NotNull()
            .WithMessage(x => ValidationMessages.User.EmailRequired)
            .NotEmpty()
            .WithMessage(x => ValidationMessages.User.EmailRequired)
            .EmailAddress()
            .WithMessage(x => ValidationMessages.User.EmailNotValid);

        RuleFor(x => x.Password)
            .NotNull()
            .WithMessage(x => ValidationMessages.User.PasswordRequired)
            .NotEmpty()
            .WithMessage(x => ValidationMessages.User.PasswordRequired);

        RuleFor(x => x.ConfirmedPassword)
            .NotNull()
            .WithMessage(x => ValidationMessages.User.ConfirmedPasswordRequired)
            .NotEmpty()
            .WithMessage(x => ValidationMessages.User.ConfirmedPasswordRequired);

        RuleFor(x => x.Password)
            .Equal(x => x.ConfirmedPassword)
            .WithMessage(x => ValidationMessages.User.PasswordNotEqualConfurmedPassword);

        RuleFor(x => x.RoleIds).NotNull().WithMessage(x => ValidationMessages.User.RolesRequired);

        RuleFor(x => x)
            .Must(req => req.RoleIds.Count != 0)
            .WithMessage(x => ValidationMessages.User.MustRolesSet);

        RuleFor(x => x)
            .Must(req => req.RoleIds.Distinct().Count() == req.RoleIds.Count)
            .WithMessage(x => ValidationMessages.User.RolesCannotbeDuplicated);

        RuleForEach(x => x.RoleIds)
            .MustAsync(
                async (roleId, cancellationToken) =>
                {
                    return await unitOfWork.Roles.AnyAsync(
                        x => x.Id == roleId,
                        cancellationToke: cancellationToken
                    );
                }
            )
            .WithMessage(x => ValidationMessages.Role.NotFound);

        RuleFor(x => x)
            .MustAsync(
                async (req, cancellationToken) =>
                {
                    return !await unitOfWork.Users.AnyAsync(
                        x => x.UserName.Trim().ToLower() == req.UserName.Trim().ToLower(),
                        cancellationToke: cancellationToken
                    );
                }
            )
            .WithMessage(x => ValidationMessages.User.UserNameExist);

        RuleFor(x => x)
            .MustAsync(
                async (req, cancellationToken) =>
                {
                    return !await unitOfWork.Users.AnyAsync(
                        x => x.Email.Trim().ToLower() == req.Email.Trim().ToLower(),
                        cancellationToke: cancellationToken
                    );
                }
            )
            .WithMessage(x => ValidationMessages.User.EmailExist);
    }
}
