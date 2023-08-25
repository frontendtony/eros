using EstateManager.Commands;
using FluentValidation;

namespace EstateManager.Validators;

public class CreateEstateCommandValidator : AbstractValidator<CreateEstateCommand>
{
    public CreateEstateCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Country).NotEmpty().WithMessage("Country is required.");
        RuleFor(x => x.State).NotEmpty().WithMessage("State is required.");
        RuleFor(x => x.City).NotEmpty().WithMessage("City is required.");
        RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required.");
        RuleFor(x => x.ZipCode).NotEmpty().WithMessage("ZipCode is required.");
    }
}
