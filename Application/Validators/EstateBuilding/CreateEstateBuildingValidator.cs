using EstateManager.Commands;
using FluentValidation;

namespace EstateManager.Validators;

public class CreateEstateBuildingCommandValidator : AbstractValidator<CreateEstateBuildingCommand>
{
    public CreateEstateBuildingCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Building name is required.");
    }
}
