using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Api.ResponseModels;
using Eros.Application.Features.Users.QueryHandlers;
using Eros.Application.Features.Users.Queries;
using Eros.Application.Features.Users.Models;

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
    [ProducesResponseType(typeof(GetUserResponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SingleResponseModel<GetUserResponseModel>>> GetUser(Guid id)
    {
        var user = await _getUserQueryHandler.Handle(new GetUserQuery(id));

        return Ok(
            new SingleResponseModel<GetUserResponseModel>()
            {
                Data = user
            }
        );
    }
}