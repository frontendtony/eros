using Microsoft.AspNetCore.Mvc;
using EstateManager.Commands;
using Api.ResponseModels;
using EstateManager.Handlers.CommandHandlers;

namespace EstateManager.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly LoginCommandHandler _loginCommandHandler;
    private readonly SignupCommandHandler _signupCommandHandler;

    public AuthController(
        LoginCommandHandler loginCommandHandler,
        SignupCommandHandler signupCommandHandler
    )
    {
        _loginCommandHandler = loginCommandHandler;
        _signupCommandHandler = signupCommandHandler;
    }

    [HttpPost("token", Name = "CreateBearerToken")]
    [ProducesResponseType(typeof(SingleResponseModel<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBearerToken([FromBody] LoginCommand request)
    {
        return Ok(await _loginCommandHandler.Handle(request));
    }

    [HttpPost("signup", Name = "Signup")]
    [ProducesResponseType(typeof(UserResponseModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Signup([FromBody] SignupCommand request)
    {
        return Ok(await _signupCommandHandler.Handle(request));
    }
}