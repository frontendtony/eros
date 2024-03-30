using Eros.Application.Features.Estates.Commands;
using FluentValidation;

namespace Eros.Application.Features.Estates.Validators;

public class CreateEstateBuildingCommandValidator : AbstractValidator<CreateEstateBuildingCommand>
{
    public CreateEstateBuildingCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Building name is required.");
        RuleFor(x => x.EstateId).NotEmpty().WithMessage("Estate id is required.");
        RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required.");
        RuleFor(x => x.BuildingTypeId).NotEmpty().WithMessage("Building type id is required.");
    }
}
