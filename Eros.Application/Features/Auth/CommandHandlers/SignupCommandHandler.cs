using Eros.Api.Dto.Auth;
using Eros.Domain.Aggregates.Users;
using Eros.Application.Features.Auth.Commands;
using FluentValidation;
using Eros.Application.Exceptions;
using Eros.Application.Features.Users.Models;
using Eros.Auth.Services;
using Mapster;
using MediatR;

namespace Eros.Application.Features.Auth.CommandHandlers;

public class SignupCommandHandler(
    JwtService jwtService,
    IUserWriteRepository userWriteRepository,
    IUserReadRepository userReadRepository,
    IValidator<SignupCommand> validator) : IRequestHandler<SignupCommand, SignupCommandDto>
{
    public async Task<SignupCommandDto> Handle(SignupCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new CustomValidationException(validationResult.Errors.Select(x => new ValidationFailure()
            {
                ErrorMessage = x.ErrorMessage
            })
            .ToList());
        }

        var existingUser = await userReadRepository.GetByEmailAsync(command.Email);

        if (existingUser is not null)
        {
            throw new ConflictException($"A user address already exists with this email address: {command.Email}");
        }

        var user = User.Create(command.Email, command.FirstName, command.LastName);

        var newUser = await userWriteRepository.AddAsync(user, command.Password, cancellationToken);
        
        if (newUser is null)
        {
            throw new BadRequestException("User could not be created");
        }
        
        var jwt = jwtService.CreateToken(newUser);
        
        var response = newUser.Adapt<SignupCommandDto>();
        response.Token = jwt.Token;
        
        return response;
    }
}
