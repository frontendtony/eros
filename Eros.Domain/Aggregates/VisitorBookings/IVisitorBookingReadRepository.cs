using Eros.Api.Dto.ApiResponseModels;

namespace Eros.Domain.Aggregates.VisitorBookings;

public interface IVisitorBookingReadRepository
{
  Task<VisitorBooking?> GetByIdAsync(Guid id);
  Task<PaginatedResponseModel<VisitorBooking>> GetEstateUserBookingsAsync(Guid userId, Guid estateId, int page, int pageSize);
}
