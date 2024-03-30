using Eros.Application.Features.Estates.Commands;
using FluentValidation;

namespace Eros.Application.Features.Estates.Validators;

public class CreateEstateCommandValidator : AbstractValidator<CreateEstateCommand>
{
    public CreateEstateCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required.");
    }
}
