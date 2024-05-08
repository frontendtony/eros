using Eros.Api.Dto.ApiResponseModels;
using Eros.Api.Dto.VisitorBookings;
using Eros.Application.Features.VisitorBookings.Queries;
using Eros.Domain.Aggregates.VisitorBookings;
using Mapster;
using MediatR;

namespace Eros.Application.Features.VisitorBookings.QueryHandlers;

public class GetUserVisitorBookingsQueryHandler(
  IVisitorBookingReadRepository visitorBookingReadRepository
) : IRequestHandler<GetUserVisitorBookingsQuery, PaginatedResponseModel<VisitorBookingDto>>
{
  public async Task<PaginatedResponseModel<VisitorBookingDto>> Handle(GetUserVisitorBookingsQuery request,
    CancellationToken cancellationToken)
  {
    var visitorBookings =
      await visitorBookingReadRepository.GetEstateUserBookingsAsync(request.UserId, request.EstateId, request.Page,
        request.PageSize);

    var visitorBookingDtos = visitorBookings.Data.Select(vb =>
      vb.Adapt<VisitorBookingDto>());

    return new PaginatedResponseModel<VisitorBookingDto>
    {
      Data = visitorBookingDtos.ToList(),
      PageNumber = visitorBookings.PageNumber,
      PageSize = visitorBookings.PageSize,
      Count = visitorBookings.Count,
      TotalPages = visitorBookings.TotalPages
    };
  }
}
