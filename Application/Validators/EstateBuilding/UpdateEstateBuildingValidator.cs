using EstateManager.Commands;
using FluentValidation;

namespace EstateManager.Validators;

public class UpdateEstateBuildingCommandValidator : AbstractValidator<UpdateEstateBuildingCommand>
{
    public UpdateEstateBuildingCommandValidator()
    {
    }
}
