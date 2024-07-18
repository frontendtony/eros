using Eros.Api.Dto.ApiResponseModels;
using Eros.Api.Dto.Auth;
using Eros.Application.Exceptions;
using Eros.Application.Features.Auth.Commands;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Eros.Api.Controllers.EstateManager.Auth;

[Route("api/auth")]
public class AuthController : ApiControllerBase
{
  [HttpPost("login")]
  [ProducesResponseType(typeof(SingleResponseModel<LoginCommandDto>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> Login(LoginDto dto)
  {
    var loginCommand = dto.Adapt<LoginCommand>();
    var loginCommandResponse = await Mediator.Send(loginCommand);

    if (loginCommandResponse.User.IsAdmin) throw new ForbiddenException("User is an admin");

    return Ok(
      new SingleResponseModel<LoginCommandDto>
      {
        Data = loginCommandResponse,
        Message = "Token created successfully"
      }
    );
  }

  [HttpPost("refresh-token")]
  [ProducesResponseType(typeof(SingleResponseModel<RefreshTokenResponseDto>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<RefreshTokenResponseDto>> RefreshToken(RefreshTokenDto dto)
  {
    var refreshTokenCommand = new RefreshTokenCommand(dto.RefreshToken, UserId);

    var refreshTokenCommandResponse = await Mediator.Send(refreshTokenCommand);

    return Ok(
      new SingleResponseModel<RefreshTokenResponseDto>
      {
        Data = refreshTokenCommandResponse,
        Message = "Token refreshed successfully"
      }
    );
  }

  [HttpPost("register")]
  [ProducesResponseType(typeof(SignupCommandDto), StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> Register(SignupDto dto)
  {
    var signupCommand = dto.Adapt<SignupCommand>();

    var signupCommandResponse = await Mediator.Send(signupCommand);

    return Ok(
      new SingleResponseModel<SignupCommandDto>
      {
        Data = signupCommandResponse,
        Message = "User created successfully"
      }
    );
  }
}
