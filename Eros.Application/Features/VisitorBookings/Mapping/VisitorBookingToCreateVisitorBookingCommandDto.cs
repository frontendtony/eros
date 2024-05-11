using Eros.Application.Features.VisitorBookings.Commands;
using Eros.Domain.Aggregates.VisitorBookings;
using Mapster;

namespace Eros.Application.Features.VisitorBookings.Mapping;

public class VisitorBookingToCreateVisitorBookingCommandDto : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<VisitorBooking, CreateVisitorBookingCommand>()
            .Map(dest => dest.Gender, src => src.Gender.ToString());
    }
}
