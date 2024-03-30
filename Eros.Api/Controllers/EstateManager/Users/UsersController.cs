using Eros.Api.Models;
using Eros.Application.Features.Users.Models;
using Eros.Application.Features.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eros.Api.Controllers.EstateManager.Users;

[ApiController]
[Route("api/users")]
[Authorize]
public class UsersController(ISender mediator) : ControllerBase
{
    [HttpGet("{id:guid}", Name = "GetUser")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SingleResponseModel<UserResponse>>> GetUser(Guid id)
    {
        var query = new GetUserQuery(id);
        var user = await mediator.Send(query);

        return Ok(
            new SingleResponseModel<UserResponse>()
            {
                Data = user
            }
        );
    }
}