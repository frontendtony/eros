using Eros.Application.Features.Invitations.Commands;
using FluentValidation;

namespace Eros.Application.Features.Invitations.Validators;

public class SendInvitationsCommandValidator : AbstractValidator<SendInvitationsCommand>
{
    public SendInvitationsCommandValidator()
    {
        RuleFor(x => x.Emails).NotEmpty().WithMessage("Emails are required");
        RuleFor(x => x.RoleId).NotEmpty().WithMessage("RoleId is required");
        RuleFor(x => x.EstateId).NotEmpty().WithMessage("EstateId is required");
        RuleFor(x => x.SenderId).NotEmpty().WithMessage("SenderId is required");
        RuleForEach(x => x.Emails).EmailAddress().WithMessage("Invalid email address");
    }
}