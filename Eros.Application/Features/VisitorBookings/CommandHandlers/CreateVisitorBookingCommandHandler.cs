using Eros.Api.Dto.VisitorBookings;
using Eros.Application.Features.VisitorBookings.Commands;
using MediatR;

namespace Eros.Application.Features.VisitorBookings.CommandHandlers;

public class CreateVisitorBookingCommandHandler
    : IRequestHandler<CreateVisitorBookingCommand, CreateVisitorBookingCommandDto>
{
    public async Task<CreateVisitorBookingCommandDto> Handle(CreateVisitorBookingCommand request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
