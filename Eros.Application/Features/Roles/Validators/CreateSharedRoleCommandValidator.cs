using Eros.Application.Features.Roles.Commands;
using FluentValidation;

namespace Eros.Application.Features.Roles.Validators;

public class CreateSharedRoleCommandValidator : AbstractValidator<CreateSharedRoleCommand>
{
    public CreateSharedRoleCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
        RuleFor(x => x.PermissionIds).NotEmpty().WithMessage("PermissionIds is required.");
    }
}