using EstateManager.Commands;
using FluentValidation;

namespace EstateManager.Validators;

public class UpdateEstateCommandValidator : AbstractValidator<UpdateEstateCommand>
{
    public UpdateEstateCommandValidator()
    {
    }
}
