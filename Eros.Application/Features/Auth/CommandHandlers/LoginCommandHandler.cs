using Eros.Api.Dto.Auth;
using Eros.Application.Exceptions;
using Eros.Application.Features.Auth.Commands;
using Eros.Auth.Services;
using Eros.Domain.Aggregates.Users;
using FluentValidation;
using MediatR;

namespace Eros.Application.Features.Auth.CommandHandlers;

public class LoginCommandHandler(
    JwtService jwtService,
    IUserReadRepository userReadRepository,
    IValidator<LoginCommand> validator) : IRequestHandler<LoginCommand, LoginCommandDto>
{
    public async Task<LoginCommandDto> Handle(LoginCommand request, CancellationToken cancellationToken)
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

        return new LoginCommandDto(jwt.Token);
    }
}
