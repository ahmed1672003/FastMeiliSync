using FastMeiliSync.Shared.Context;

namespace FastMeiliSync.Application.Features.Users.Commands.Update;

public sealed class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    private readonly IServiceProvider _serviceProvider;

    public UpdateUserValidator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        var scope = _serviceProvider.CreateScope();
        ValidateRequest(
            scope.ServiceProvider.GetRequiredService<IMeiliSyncUnitOfWork>(),
            scope.ServiceProvider.GetRequiredService<IUserContext>()
        );
    }

    void ValidateRequest(IMeiliSyncUnitOfWork unitOfWork, IUserContext userContext)
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

        RuleFor(x => x.RoleIds).NotNull().WithMessage(x => ValidationMessages.User.RolesRequired);

        RuleFor(x => x)
            .Must(req => req.RoleIds.Count != 0)
            .WithMessage(x => ValidationMessages.User.MustRolesSet);

        RuleFor(x => x)
            .Must(req => req.RoleIds.Distinct().Count() == req.RoleIds.Count)
            .WithMessage(x => ValidationMessages.User.RolesCannotbeDuplicated);

        RuleFor(x => x)
            .MustAsync(
                async (req, cancellationToken) =>
                {
                    return !await unitOfWork.Users.AnyAsync(x => x.Id == req.Id && x.Master);
                }
            )
            .WithMessage(x => ValidationMessages.User.CannotUpdateUser);

        RuleFor(x => x)
            .MustAsync(
                async (req, cancellationToken) =>
                {
                    return await unitOfWork.Users.AnyAsync(x =>
                            x.Id == userContext.UserId.Value && x.Master
                        )
                        || req.Id == userContext.UserId.Value;
                }
            )
            .WithMessage(x => ValidationMessages.User.CannotUpdateUser);

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
                        x =>
                            x.UserName.Trim().ToLower() == req.UserName.Trim().ToLower()
                            && x.Id != req.Id,
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
                        x =>
                            x.Email.Trim().ToLower() == req.Email.Trim().ToLower()
                            && x.Id != req.Id,
                        cancellationToke: cancellationToken
                    );
                }
            )
            .WithMessage(x => ValidationMessages.User.EmailExist);
    }
}
