using Eros.Application.Features.Invitations.Commands;
using Eros.Domain.Aggregates.Invitations;
using FluentValidation;

namespace Eros.Application.Features.Invitations.Validators;

public class AcceptInvitationCommandValidator : AbstractValidator<AcceptInvitationCommand>
{
    public AcceptInvitationCommandValidator()
    {
        RuleFor(x => x.Code).NotEmpty().WithMessage("Invitation code is required");
        RuleFor(x => x.Status)
            .IsInEnum()
            .Must(x => x is InvitationStatus.Accepted or InvitationStatus.Rejected)
            .WithMessage("Invalid status. Status must be 'Accepted' or 'Rejected'");
    }
}