using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Eros.Api.Dto.Auth;
using Eros.Application.Exceptions;
using Eros.Application.Features.Auth.Commands;
using Eros.Auth.Services;
using Eros.Domain.Aggregates.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Eros.Application.Features.Auth.CommandHandlers;

public class RefreshTokenCommandHandler(
  IUserReadRepository userReadRepository,
  IUserWriteRepository userWriteRepository,
  JwtService jwtService,
  ILogger<RefreshTokenCommandHandler> logger) : IRequestHandler<RefreshTokenCommand, RefreshTokenResponseDto>
{
  public async Task<RefreshTokenResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
  {
    logger.LogInformation("Refreshing token for user {UserId}", request.UserId);

    var securityToken = jwtService.ValidateRefreshToken(request.RefreshToken);

    if (securityToken is null)
    {
      logger.LogError("Jwt validation failed for user {UserId}", request.UserId);
      throw new BadRequestException("Invalid or expired refresh token");
    }

    var jwtUserId = securityToken.Subject;

    if (jwtUserId is null)
    {
      logger.LogError("UserId not found in refresh token for user {UserId}", request.UserId);
      throw new BadRequestException("Invalid refresh token");
    }

    if (jwtUserId != request.UserId.ToString())
    {
      logger.LogError("UserId mismatch for user {UserId}", request.UserId);
      throw new BadRequestException("Invalid user id");
    }

    var user = await userReadRepository.GetByIdAsync(request.UserId, cancellationToken);

    if (user is null)
    {
      logger.LogError("User not found for user {UserId}", request.UserId);
      throw new UnauthorizedException("User not found");
    }

    if (user.RefreshToken != request.RefreshToken)
    {
      logger.LogError("Refresh token mismatch for user {UserId}", request.UserId);
      throw new BadRequestException("Invalid refresh token");
    }

    var jwt = await jwtService.CreateToken(user);

    user.RefreshToken = jwt.RefreshToken;

    await userWriteRepository.UpdateAsync(user, cancellationToken);
    await userWriteRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

    return new RefreshTokenResponseDto(jwt.Token, jwt.ExpiresAt, jwt.RefreshToken);
  }
}
