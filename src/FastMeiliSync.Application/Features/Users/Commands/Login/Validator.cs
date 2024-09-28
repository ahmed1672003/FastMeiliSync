using Microsoft.AspNetCore.Identity;

namespace FastMeiliSync.Application.Features.Users.Commands.Create;

public sealed class LogInUserValidator : AbstractValidator<LogInUserCommand>
{
    private readonly IServiceProvider _serviceProvider;

    public LogInUserValidator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        var scope = _serviceProvider.CreateScope();
        IPasswordHasher<User> passwordValidator = scope.ServiceProvider.GetRequiredService<
            IPasswordHasher<User>
        >();

        IMeiliSyncUnitOfWork unitOfWork =
            scope.ServiceProvider.GetRequiredService<IMeiliSyncUnitOfWork>();
        ValidateRequest(unitOfWork, passwordValidator);
    }

    void ValidateRequest(IMeiliSyncUnitOfWork unitOfWork, IPasswordHasher<User> passwordValidator)
    {
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

        RuleFor(x => x)
            .MustAsync(
                async (req, cancellationToken) =>
                {
                    return await unitOfWork.Users.AnyAsync(
                        x => x.Email.Trim().ToLower() == req.Email.Trim().ToLower(),
                        cancellationToke: cancellationToken
                    );
                }
            )
            .WithMessage(x => ValidationMessages.User.EmailNotExist);

        RuleFor(x => x)
            .MustAsync(
                async (req, cancellationToken) =>
                {
                    User user = await unitOfWork.Users.GetAsync<User>(x =>
                        x.Email.Trim().ToLower() == req.Email.Trim().ToLower()
                    );

                    return passwordValidator.VerifyHashedPassword(
                            user,
                            user.HashedPassword,
                            req.Password
                        ) == PasswordVerificationResult.Success;
                }
            )
            .WithMessage(x => ValidationMessages.User.EmailNotExist);
    }
}
