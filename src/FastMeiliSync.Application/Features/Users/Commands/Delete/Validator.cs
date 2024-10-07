using FastMeiliSync.Shared.Context;

namespace FastMeiliSync.Application.Features.Users.Commands.Delete;

public sealed class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
{
    private readonly IServiceProvider _serviceProvider;

    public DeleteUserValidator(IServiceProvider serviceProvider)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        ClassLevelCascadeMode = CascadeMode.Stop;

        _serviceProvider = serviceProvider;
        var scope = _serviceProvider.CreateScope();
        ValidateRequest(
            scope.ServiceProvider.GetRequiredService<IMeiliSyncUnitOfWork>(),
            scope.ServiceProvider.GetRequiredService<IUserContext>()
        );
    }

    void ValidateRequest(IMeiliSyncUnitOfWork unitOfWork, IUserContext userContext)
    {
        RuleFor(x => x)
            .MustAsync(
                async (req, cancellationToken) =>
                {
                    return !await unitOfWork.Users.AnyAsync(x => x.Id == req.Id && x.Master);
                }
            )
            .WithMessage(x => ValidationMessages.User.CannotDeleteUser);

        RuleFor(x => x)
            .MustAsync(
                async (req, cancellationToken) =>
                {
                    return req.Id == userContext.UserId.Value
                        || await unitOfWork.Users.AnyAsync(x =>
                            x.Id == userContext.UserId.Value && x.Master
                        );
                }
            )
            .WithMessage(x => ValidationMessages.User.CannotDeleteUser);

        RuleFor(x => x)
            .MustAsync(
                async (req, cancellationToken) =>
                {
                    return await unitOfWork.Users.AnyAsync(x => x.Id == req.Id);
                }
            )
            .WithMessage(x => ValidationMessages.User.NotFound);
    }
}
