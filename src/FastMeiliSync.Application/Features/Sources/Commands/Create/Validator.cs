using FastMeiliSync.Application.Features.Sources.Commands.Create;

public class CreateSourceValidator : AbstractValidator<CreateSourceCommand>
{
    public CreateSourceValidator()
    {
        RuleFor(x => x.Label).NotEmpty();
    }
}
