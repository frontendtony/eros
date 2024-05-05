using Eros.Api.Dto.VisitorBookings;
using Eros.Application.Features.VisitorBookings.Commands;
using Eros.Domain.Aggregates.VisitorBookings;
using Eros.Persistence;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Eros.Application.Features.VisitorBookings.CommandHandlers;

public class CreateVisitorBookingCommandHandler(
    ErosDbContext dbContext,
    IVisitorBookingWriteRepository visitorBookingWriteRepository,
    ILogger<CreateVisitorBookingCommandHandler> logger
)
    : IRequestHandler<CreateVisitorBookingCommand, CreateVisitorBookingCommandDto>
{
    public async Task<CreateVisitorBookingCommandDto> Handle(CreateVisitorBookingCommand command,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating visitor booking");

        var visitorBooking = command.Adapt<VisitorBooking>();
        visitorBooking = await visitorBookingWriteRepository.CreateAsync(visitorBooking);

        await dbContext.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Visitor booking created");

        return visitorBooking.Adapt<CreateVisitorBookingCommandDto>();
    }
}
