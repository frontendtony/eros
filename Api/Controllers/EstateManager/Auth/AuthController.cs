using Microsoft.AspNetCore.Mvc;
using Api.ResponseModels;
using Eros.Application.Features.Users.CommandHandlers;
using Eros.Application.Features.Users.Commands;
using Eros.Application.Features.Users.Models;

namespace Eros.Api.Controllers;

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
        var loginCommandResponse = await _loginCommandHandler.Handle(request);

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
        var signupCommandResponse = await _signupCommandHandler.Handle(request);

        return Ok(
            new SingleResponseModel<SignupCommandResponse>()
            {
                Data = signupCommandResponse,
                Message = "User created successfully"
            }
        );
    }
}