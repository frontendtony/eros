using Eros.Domain.Aggregates.VisitorBookings;

namespace Eros.Persistence.Data.VisitorBookings.Repositories;

public class VisitorBookingWriteRepository(
    ErosDbContext dbContext
) : IVisitorBookingWriteRepository
{
    public async Task<VisitorBooking> CreateAsync(VisitorBooking visitorBooking)
    {
        await dbContext.VisitorBookings.AddAsync(visitorBooking);
        return visitorBooking;
    }

    public VisitorBooking UpdateAsync(VisitorBooking visitorBooking)
    {
        dbContext.VisitorBookings.Update(visitorBooking);
        return visitorBooking;
    }

    public Task DeleteAsync(VisitorBooking visitorBooking)
    {
        dbContext.VisitorBookings.Remove(visitorBooking);
        return Task.CompletedTask;
    }
}
