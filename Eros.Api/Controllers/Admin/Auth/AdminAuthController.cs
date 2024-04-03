using Eros.Api.Dto.Auth;
using Eros.Api.Models;
using Eros.Application.Exceptions;
using Eros.Application.Features.Auth.Commands;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eros.Api.Controllers.Admin.Auth;

[ApiController]
[Route("api/admin/auth")]
public class AdminAuthController(ISender mediator) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var loginCommand = dto.Adapt<LoginCommand>();
        var loginCommandResponse = await mediator.Send(loginCommand);
        
        var loginCommandDto = loginCommandResponse.Value;
        if (!loginCommandDto.User.IsAdmin)
        {
            throw new UnauthorizedException("User is not an admin");
        }
        
        return Ok(
            new SingleResponseModel<LoginCommandDto>()
            {
                Data = loginCommandResponse.Value,
                Message = "Token created successfully"
            }
        );
    }
}