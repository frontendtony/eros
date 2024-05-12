using Eros.Api.Dto.ApiResponseModels;
using Eros.Api.Dto.VisitorBookings;
using MediatR;

namespace Eros.Application.Features.VisitorBookings.Queries;

public record GetUserVisitorBookingsQuery : IRequest<PaginatedResponseModel<VisitorBookingDto>>
{
    public Guid UserId { get; set; }
    public Guid EstateId { get; set; }
    public int Page { get; init; }
    public int PageSize { get; init; }
}
