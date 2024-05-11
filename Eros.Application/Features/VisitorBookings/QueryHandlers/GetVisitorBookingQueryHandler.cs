using Eros.Api.Dto.VisitorBookings;
using Eros.Application.Exceptions;
using Eros.Application.Features.VisitorBookings.Queries;
using Eros.Domain.Aggregates.VisitorBookings;
using Mapster;
using MediatR;

namespace Eros.Application.Features.VisitorBookings.QueryHandlers;

public class GetVisitorBookingQueryHandler(
    IVisitorBookingReadRepository visitorBookingReadRepository
) : IRequestHandler<GetVisitorBookingQuery, VisitorBookingDto>
{
    public async Task<VisitorBookingDto> Handle(GetVisitorBookingQuery query, CancellationToken cancellationToken)
    {
        var visitorBooking = await visitorBookingReadRepository.GetByIdAsync(query.Id)
            ?? throw new NotFoundException("Visitor booking not found");

        return visitorBooking.Adapt<VisitorBookingDto>();
    }
}
