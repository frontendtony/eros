using Eros.Api.Dto.VisitorBookings;
using Eros.Domain.Aggregates.VisitorBookings;
using Mapster;

namespace Eros.Application.Features.VisitorBookings.Mapping;

public class VisitorBookingToVisitorBookingDto : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<VisitorBooking, VisitorBookingDto>()
            .Map(dest => dest.Id, src => src.Id.ToString())
            .Map(dest => dest.EstateId, src => src.EstateId.ToString())
            .Map(dest => dest.CreatedBy, src => src.CreatedBy.ToString())
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Purpose, src => src.Purpose)
            .Map(dest => dest.RejectionReason, src => src.RejectionReason)
            .Map(dest => dest.Gender, src => src.Gender.ToString())
            .Map(dest => dest.Status, src => src.Status.ToString())
            .Map(dest => dest.UpdatedBy, src => src.UpdatedBy.ToString())
            .Map(dest => dest.IsDeleted, src => src.IsDeleted);
    }
}
