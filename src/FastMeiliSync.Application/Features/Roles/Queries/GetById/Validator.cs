namespace FastMeiliSync.Application.Features.Roles.Queries.GetById;

public sealed class GetRoleByIdValidator : AbstractValidator<GetRoleByIdQuery>
{
    private readonly IServiceProvider _serviceProvider;

    public GetRoleByIdValidator(IServiceProvider serviceProvider)
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
                    return await unitOfWork.Roles.AnyAsync(x => x.Id == req.Id);
                }
            )
            .WithMessage(x => ValidationMessages.Role.NotFound);
    }
}
