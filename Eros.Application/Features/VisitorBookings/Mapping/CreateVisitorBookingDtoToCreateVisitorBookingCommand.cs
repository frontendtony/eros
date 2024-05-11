using Eros.Api.Dto.VisitorBookings;
using Eros.Application.Features.VisitorBookings.Commands;
using Eros.Domain.Enums;
using Mapster;

namespace Eros.Application.Features.VisitorBookings.Mapping;

public class CreateVisitorBookingDtoToCreateVisitorBookingCommand : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateVisitorBookingDto, CreateVisitorBookingCommand>()
            .Map(dest => dest.Gender, src => Enum.Parse<Gender>(src.Gender));
    }
}
