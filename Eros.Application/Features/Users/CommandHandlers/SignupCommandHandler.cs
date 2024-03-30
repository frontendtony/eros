using Eros.Domain.Aggregates.Users;
using Eros.Application.Features.Users.Commands;
using FluentValidation;
using Eros.Application.Features.Users.Models;
using Eros.Application.Exceptions;
using Eros.Auth.Services;

namespace Eros.Application.Features.Users.CommandHandlers;

public class SignupCommandHandler(
    JwtService jwtService,
    IUserWriteRepository userWriteRepository,
    IUserReadRepository userReadRepository,
    IValidator<SignupCommand> validator)
{
    public async Task<SignupCommandResponse> Handle(SignupCommand command)
    {
        var validationResult = await validator.ValidateAsync(command);

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

        var user = User.Create(command.FirstName, command.LastName, command.Email, command.Avatar);

        var newUser = await userWriteRepository.AddAsync(user, command.Password) ?? throw new BadRequestException("Error creating user");
        var jwt = jwtService.CreateToken(newUser);

        return new SignupCommandResponse(
            jwt.Token,
            newUser.Email,
            newUser.FirstName,
            newUser.LastName,
            newUser.Avatar
        );
    }
}
