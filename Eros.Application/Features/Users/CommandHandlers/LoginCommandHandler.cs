using FluentValidation;
using Eros.Application.Features.Users.Commands;
using Eros.Application.Exceptions;
using Eros.Domain.Aggregates.Users;
using Application.Features.Users.Models;
using Eros.Auth.Services;
using MediatR;

namespace Eros.Application.Features.Users.CommandHandlers;

public class LoginCommandHandler(
    JwtService jwtService,
    IUserReadRepository userReadRepository,
    IValidator<LoginCommand> validator) : IRequestHandler<LoginCommand, LoginResponse>
{
    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new CustomValidationException(validationResult.Errors.Select(x => new ValidationFailure()
            {
                ErrorMessage = x.ErrorMessage
            })
            .ToList());
        }

        var user = await userReadRepository.GetByEmailAsync(request.Email);

        if (user is null)
        {
            throw new BadRequestException("Incorrect email address");
        }


        if (user.IsAdmin)
        {
            throw new ForbiddenException("User is an admin");
        }

        var isPasswordValid = await userReadRepository.CheckPassword(user, request.Password);

        if (!isPasswordValid)
        {
            throw new BadRequestException("Password is invalid");
        }

        var jwt = jwtService.CreateToken(user);

        return new LoginResponse(jwt.Token);
    }
}
