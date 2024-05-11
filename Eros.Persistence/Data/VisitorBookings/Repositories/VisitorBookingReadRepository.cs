using Eros.Api.Dto.ApiResponseModels;
using Eros.Domain.Aggregates.VisitorBookings;
using Eros.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Eros.Persistence.Data.VisitorBookings.Repositories;

public class VisitorBookingReadRepository(
  ErosDbContext dbContext
) : IVisitorBookingReadRepository
{
  public async Task<VisitorBooking?> GetByIdAsync(Guid id)
  {
    return await dbContext.VisitorBookings
      .Where(vb => vb.Id == id && vb.IsDeleted == false)
      .FirstOrDefaultAsync();
  }

  public async Task<PaginatedResponseModel<VisitorBooking>> GetEstateUserBookingsAsync(
    Guid userId, Guid estateId, int page, int pageSize)
  {
    return await dbContext.VisitorBookings
      .AsNoTracking()
      .Where(vb => vb.CreatedBy == userId && vb.EstateId == estateId && vb.IsDeleted == false)
      .OrderByDescending(vb => vb.CreatedAt)
      .ToPagedResultAsync(page, pageSize);
  }
}
