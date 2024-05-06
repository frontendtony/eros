namespace Eros.Domain.Aggregates.VisitorBookings;

public interface IVisitorBookingReadRepository
{
    Task<VisitorBooking?> GetByIdAsync(Guid id);
    Task<IEnumerable<VisitorBooking>> GetEstateUserBookingsAsync(Guid userId, Guid estateId);
}
