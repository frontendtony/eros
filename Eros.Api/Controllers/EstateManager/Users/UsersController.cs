using Eros.Api.Attributes;
using Eros.Api.Dto.ApiResponseModels;
using Eros.Api.Dto.VisitorBookings;
using Eros.Application.Exceptions;
using Eros.Application.Features.Users.Models;
using Eros.Application.Features.Users.Queries;
using Eros.Application.Features.VisitorBookings.Queries;
using Eros.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eros.Api.Controllers.EstateManager.Users;

[ApiController]
[Route("api/users")]
[Authorize]
public class UsersController : EstateManagerControllerBase
{
    [HttpGet("{id:guid}", Name = "GetUser")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SingleResponseModel<UserResponse>>> GetUser(Guid id)
    {
        var query = new GetUserQuery(id);
        var user = await Mediator.Send(query);

        return Ok(
            new SingleResponseModel<UserResponse>
            {
                Data = user
            }
        );
    }

    [HttpGet("{id:guid}/visitor-bookings")]
    public async Task<ActionResult<PaginatedResponseModel<VisitorBookingDto>>> GetUserVisitorBookings(
        Guid id,
        [FromQuery] PaginationQueryModel queryParams
    )
    {
        var estateId = EstateId ?? throw new BadRequestException("EstateId is required");

        var query = new GetUserVisitorBookingsQuery
        {
            UserId = id,
            EstateId = estateId,
            Page = queryParams.PageNumber,
            PageSize = queryParams.PageSize
        };
        var visitorBookings = await Mediator.Send(query);

        return Ok(visitorBookings);
    }
}
