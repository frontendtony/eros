using Eros.Api.Dto.Auth;
using Eros.Api.Models;
using Eros.Application.Exceptions;
using Eros.Application.Features.Auth.Commands;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Eros.Api.Controllers.Admin.Auth;

[Route("api/admin/auth")]
public class AdminAuthController : ApiControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var loginCommand = dto.Adapt<LoginCommand>();
        var loginCommandResponse = await Mediator.Send(loginCommand);
        
        if (!loginCommandResponse.User.IsAdmin)
        {
            throw new UnauthorizedException("User is not an admin");
        }
        
        return Ok(
            new SingleResponseModel<LoginCommandDto>()
            {
                Data = loginCommandResponse,
                Message = "Token created successfully"
            }
        );
    }
}