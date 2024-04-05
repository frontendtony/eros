using Eros.Api.Dto.Estates;
using Eros.Domain.Aggregates.Estates;
using Mapster;

namespace Eros.Application.Mapping.Estates;

public class EstateToCreateEstateCommandDto : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<Estate, CreateEstateCommandDto>()
            .Map(dest => dest.Id, src => src.Id.ToString())
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Address, src => src.Address);
    }
}