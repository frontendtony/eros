using EstateManager.Commands;
using FluentValidation;

namespace EstateManager.Validators;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
    }
}
