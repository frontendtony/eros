using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Api.ResponseModels;
using EstateManager.Handlers.QueryHandlers;

namespace EstateManager.Controllers;

[ApiController]
[Route("api/users")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly GetUserQueryHandler _getUserQueryHandler;

    public UsersController(GetUserQueryHandler getUserQueryHandler)
    {
        _getUserQueryHandler = getUserQueryHandler;
    }

    [HttpGet("{id:guid}", Name = "GetUser")]
    [ProducesResponseType(typeof(UserResponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SingleResponseModel<UserResponseModel>>> GetUser(Guid id)
    {
        return Ok(await _getUserQueryHandler.Handle(id));
    }
}