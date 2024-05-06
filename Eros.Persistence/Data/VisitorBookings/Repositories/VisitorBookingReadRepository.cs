using Eros.Domain.Aggregates.VisitorBookings;
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

    public async Task<IEnumerable<VisitorBooking>> GetEstateUserBookingsAsync(Guid userId, Guid estateId)
    {
        return await dbContext.VisitorBookings
            .Where(vb => vb.CreatedBy == userId && vb.EstateId == estateId && vb.IsDeleted == false)
            .ToListAsync();
    }
}
