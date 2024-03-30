using Eros.Api.Models;
using Eros.Application.Features.Users.CommandHandlers;
using Eros.Application.Features.Users.Commands;
using Eros.Application.Features.Users.Models;
using Microsoft.AspNetCore.Mvc;

namespace Eros.Api.Controllers.EstateManager.Auth;

[ApiController]
[Route("api/auth")]
public class AuthController(
    LoginCommandHandler loginCommandHandler,
    SignupCommandHandler signupCommandHandler)
    : ControllerBase
{
    [HttpPost("token", Name = "CreateBearerToken")]
    [ProducesResponseType(typeof(SingleResponseModel<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBearerToken([FromBody] LoginCommand request)
    {
        var loginCommandResponse = await loginCommandHandler.Handle(request);

        return Ok(
            new SingleResponseModel<string>()
            {
                Data = loginCommandResponse.Token,
                Message = "Token created successfully"
            }
        );
    }

    [HttpPost("signup", Name = "Signup")]
    [ProducesResponseType(typeof(SignupCommandResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Signup([FromBody] SignupCommand request)
    {
        var signupCommandResponse = await signupCommandHandler.Handle(request);

        return Ok(
            new SingleResponseModel<SignupCommandResponse>()
            {
                Data = signupCommandResponse,
                Message = "User created successfully"
            }
        );
    }
}