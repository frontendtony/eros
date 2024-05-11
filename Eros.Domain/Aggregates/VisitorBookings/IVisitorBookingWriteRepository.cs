namespace Eros.Domain.Aggregates.VisitorBookings;

public interface IVisitorBookingWriteRepository
{
    Task<VisitorBooking> CreateAsync(VisitorBooking visitorBooking);
    VisitorBooking UpdateAsync(VisitorBooking visitorBooking);
    Task DeleteAsync(VisitorBooking visitorBooking);
}
