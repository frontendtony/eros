using Eros.Api.Dto.VisitorBookings;
using MediatR;

namespace Eros.Application.Features.VisitorBookings.Queries;

public record GetVisitorBookingQuery(Guid Id) : IRequest<VisitorBookingDto>;
