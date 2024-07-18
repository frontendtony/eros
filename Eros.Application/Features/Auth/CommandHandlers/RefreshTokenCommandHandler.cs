using Eros.Api.Dto.Auth;
using Eros.Application.Exceptions;
using Eros.Application.Features.Auth.Commands;
using Eros.Auth.Services;
using Eros.Domain.Aggregates.Users;
using MediatR;

namespace Eros.Application.Features.Auth.CommandHandlers;

public class RefreshTokenCommandHandler(
  IUserReadRepository userReadRepository,
  IUserWriteRepository userWriteRepository,
  JwtService jwtService
) : IRequestHandler<RefreshTokenCommand, RefreshTokenResponseDto>
{
  public async Task<RefreshTokenResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
  {
    var user = await userReadRepository.GetByIdAsync(request.UserId, cancellationToken)
               ?? throw new BadRequestException("Invalid user id");

    if (user.RefreshToken != request.RefreshToken) throw new BadRequestException("Invalid refresh token");

    var jwt = await jwtService.CreateToken(user);

    user.RefreshToken = jwt.RefreshToken;

    await userWriteRepository.UpdateAsync(user, cancellationToken);
    await userWriteRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

    return new RefreshTokenResponseDto(jwt.Token, jwt.ExpiresAt, jwt.RefreshToken);
  }
}
