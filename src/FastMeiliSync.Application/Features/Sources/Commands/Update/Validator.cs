namespace FastMeiliSync.Application.Features.Sources.Commands.Update;

public sealed class UpdateSourceValidator : AbstractValidator<UpdateSourceCommand>
{
    private readonly IServiceProvider _serviceProvider;

    public UpdateSourceValidator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        var scope = _serviceProvider.CreateScope();
        ValidateRequest(scope.ServiceProvider.GetRequiredService<IMeiliSyncUnitOfWork>());
    }

    void ValidateRequest(IMeiliSyncUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Database)
            .NotNull()
            .WithMessage(x => ValidationMessages.Source.DatabaseRequired)
            .NotEmpty()
            .WithMessage(x => ValidationMessages.Source.DatabaseRequired);

        RuleFor(x => x.Label)
            .NotNull()
            .WithMessage(x => ValidationMessages.Source.LabelRequired)
            .NotEmpty()
            .WithMessage(x => ValidationMessages.Source.LabelRequired);

        RuleFor(x => x.Url)
            .NotNull()
            .WithMessage(x => ValidationMessages.Source.UrlRequired)
            .NotEmpty()
            .WithMessage(x => ValidationMessages.Source.UrlRequired);

        RuleFor(x => x)
            .Must(req => req.Type == SourceType.PostgresSQL)
            .WithMessage(x => ValidationMessages.Source.UnSuportedType);

        RuleFor(x => x)
            .MustAsync(
                async (req, cancellationToken) =>
                {
                    return await unitOfWork.Sources.AnyAsync(x => x.Id == req.Id);
                }
            )
            .WithMessage(x => ValidationMessages.Source.NotFound);

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    return !await unitOfWork.Sources.AnyAsync(x =>
                        x.Label.Trim().ToLower() == req.Label.Trim().ToLower() && x.Id != req.Id
                    );
                }
            )
            .WithMessage(x => ValidationMessages.Source.LabelExist);

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    return !await unitOfWork.Sources.AnyAsync(x =>
                        x.Database.Trim().ToLower() == req.Database.Trim().ToLower()
                        && x.Id != req.Id
                    );
                }
            )
            .WithMessage(x => ValidationMessages.Source.DatabaseExist);

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    return !await unitOfWork.Sources.AnyAsync(x =>
                        x.Url.Trim().ToLower() == req.Url.Trim().ToLower() && x.Id != req.Id
                    );
                }
            )
            .WithMessage(x => ValidationMessages.Source.UrlExist);
    }
}
