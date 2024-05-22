using Eros.Api.Dto.Auth;
using Eros.Application.Exceptions;
using Eros.Application.Features.Auth.Commands;
using Eros.Auth.Services;
using Eros.Domain.Aggregates.Users;
using Mapster;
using MediatR;

namespace Eros.Application.Features.Auth.CommandHandlers;

public class SignupCommandHandler(
  JwtService jwtService,
  IUserWriteRepository userWriteRepository,
  IUserReadRepository userReadRepository) : IRequestHandler<SignupCommand, SignupCommandDto>
{
  public async Task<SignupCommandDto> Handle(SignupCommand command, CancellationToken cancellationToken)
  {
    var existingUser = await userReadRepository.GetByEmailAsync(command.Email);

    if (existingUser is not null)
      throw new ConflictException($"A user address already exists with this email address: {command.Email}");

    var user = User.Create(command.Email, command.FirstName, command.LastName);

    var newUser = await userWriteRepository.AddAsync(user, command.Password, cancellationToken)
                  ?? throw new BadRequestException("User could not be created");

    var jwt = await jwtService.CreateToken(newUser);

    var userDto = newUser.Adapt<UserDto>();
    var response = new SignupCommandDto(jwt.Token, jwt.ExpiresAt, userDto);

    return response;
  }
}
