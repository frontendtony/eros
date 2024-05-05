using Eros.Api.Attributes;
using Eros.Api.Dto.VisitorBookings;
using Eros.Api.Models;
using Eros.Application.Exceptions;
using Eros.Application.Features.VisitorBookings.Commands;
using Eros.Common.Constants;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eros.Api.Controllers.EstateManager.VisitorBookings;

[Route("/api/visitor-bookings")]
[Authorize]
[RequirePermission(PermissionConstants.CreateVisitorBooking)]
public class VisitorBookingsController : EstateManagerControllerBase
{
    [HttpPost]
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
}
