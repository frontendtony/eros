using Eros.Api.Attributes;
using Eros.Api.Dto.ApiResponseModels;
using Eros.Api.Dto.VisitorBookings;
using Eros.Application.Exceptions;
using Eros.Application.Features.VisitorBookings.Commands;
using Eros.Application.Features.VisitorBookings.Queries;
using Eros.Common.Constants;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eros.Api.Controllers.EstateManager.VisitorBookings;

[Route("/api/visitor-bookings")]
[Authorize]
public class VisitorBookingsController : EstateManagerControllerBase
{
    [HttpPost]
    [RequirePermission(PermissionConstants.CreateVisitorBooking)]
    public async Task<ActionResult<SingleResponseModel<CreateVisitorBookingCommandDto>>> CreateVisitorBookingAsync(
        CreateVisitorBookingDto request)
    {
        if (EstateId is null) throw new BadRequestException("EstateId is required");

        var command = request.Adapt<CreateVisitorBookingCommand>();

        command.EstateId = EstateId.Value;
        command.CreatedBy = UserId;

        var visitorBooking = await Mediator.Send(command);

        return new SingleResponseModel<CreateVisitorBookingCommandDto>
        {
            Data = visitorBooking
        };
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SingleResponseModel<VisitorBookingDto>>> GetVisitorBookingAsync(Guid id)
    {
        var visitorBooking = await Mediator.Send(new GetVisitorBookingQuery(id));

        return new SingleResponseModel<VisitorBookingDto>
        {
            Data = visitorBooking
        };
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResponseModel<VisitorBookingDto>>> GetVisitorBookingsAsync(
        [FromQuery] PaginationQueryModel queryParams
    )
    {
        var estateId = EstateId ?? throw new BadRequestException("EstateId is required");

        var query = new GetUserVisitorBookingsQuery
        {
            UserId = UserId,
            EstateId = estateId,
            Page = queryParams.PageNumber,
            PageSize = queryParams.PageSize
        };
        var visitorBookings = await Mediator.Send(query);

        return Ok(visitorBookings);
    }
}
