using Eros.Application.Features.Estates.Commands;
using FluentValidation;

namespace Eros.Application.Features.Estates.Validators;

public class UpdateEstateCommandValidator : AbstractValidator<UpdateEstateCommand>
{
    public UpdateEstateCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required.");
    }
}
