using Eros.Application.Features.Estates.Commands;
using FluentValidation;

namespace Eros.Application.Features.Estates.Validators;

public class UpdateEstateBuildingCommandValidator : AbstractValidator<UpdateEstateBuildingCommand>
{
    public UpdateEstateBuildingCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Building id is required.");
    }
}
