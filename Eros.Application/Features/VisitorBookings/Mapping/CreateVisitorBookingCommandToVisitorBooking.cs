using Eros.Application.Features.VisitorBookings.Commands;
using Eros.Domain.Aggregates.VisitorBookings;
using Mapster;

namespace Eros.Application.Features.VisitorBookings.Mapping;

public class CreateVisitorBookingCommandToVisitorBooking : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateVisitorBookingCommand, VisitorBooking>();
    }
}
