using Eros.Application.Features.VisitorBookings.Commands;
using FluentValidation;

namespace Eros.Application.Features.VisitorBookings.Validators;

public class CreateVisitorBookingCommandValidator : AbstractValidator<CreateVisitorBookingCommand>
{
    public CreateVisitorBookingCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Gender).IsInEnum();
        RuleFor(x => x.Purpose).MaximumLength(200);
        RuleFor(x => x.PhoneNumber).Length(11);
    }
}
