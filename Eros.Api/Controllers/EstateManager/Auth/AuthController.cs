using Eros.Api.Contracts.Users;
using Eros.Api.Models;
using Eros.Application.Features.Users.Commands;
using Eros.Application.Features.Users.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eros.Api.Controllers.EstateManager.Auth;

[ApiController]
[Route("api/auth")]
public class AuthController(ISender mediator) : ControllerBase
{
    [HttpPost("token", Name = "CreateBearerToken")]
    [ProducesResponseType(typeof(SingleResponseModel<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBearerToken([FromBody] LoginRequest request)
    {
        var loginCommand = new LoginCommand()
        {
            Email = request.Email,
            Password = request.Password
        };
        var loginCommandResponse = await mediator.Send(loginCommand);

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
    public async Task<IActionResult> Signup([FromBody] SignupRequest request)
    {
        var signupCommand = new SignupCommand(
            request.Email,
            request.Password,
            request.FirstName,
            request.LastName,
            request.Avatar
        );
        
        var signupCommandResponse = await mediator.Send(signupCommand);

        return Ok(
            new SingleResponseModel<SignupCommandResponse>()
            {
                Data = signupCommandResponse,
                Message = "User created successfully"
            }
        );
    }
}