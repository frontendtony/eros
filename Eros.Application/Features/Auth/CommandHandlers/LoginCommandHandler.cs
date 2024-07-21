using Eros.Api.Dto.Auth;
using Eros.Application.Exceptions;
using Eros.Application.Features.Auth.Commands;
using Eros.Auth.Services;
using Eros.Common.Utils;
using Eros.Domain.Aggregates.Users;
using Mapster;
using MediatR;

namespace Eros.Application.Features.Auth.CommandHandlers;

public class LoginCommandHandler(
  JwtService jwtService,
  IUserWriteRepository userWriteRepository,
  IUserReadRepository userReadRepository) : IRequestHandler<LoginCommand, LoginCommandDto>
{
  public async Task<LoginCommandDto> Handle(LoginCommand request, CancellationToken cancellationToken)
  {
    var user = await userReadRepository.GetByEmailAsync(request.Email)
               ?? throw new BadRequestException("Incorrect email address");

    var isPasswordValid = await userReadRepository.CheckPassword(user, request.Password);

    if (!isPasswordValid) throw new BadRequestException("Password is invalid");

    var jwt = await jwtService.CreateToken(user);

    var hashedRefreshToken = Crypto.Hash(jwt.RefreshToken);

    user.RefreshToken = hashedRefreshToken;

    await userWriteRepository.UpdateAsync(user, cancellationToken);
    await userWriteRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

    var userDto = user.Adapt<UserDto>();

    return new LoginCommandDto(jwt.Token, jwt.ExpiresAt, userDto, jwt.RefreshToken);
  }
}
