using Eros.Api.Dto.Roles;
using Eros.Domain.Aggregates.Roles;
using Mapster;

namespace Eros.Application.Mapping.Roles;

public class RoleToCreateSharedRoleCommandDto : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Role, CreateSharedRoleCommandDto>()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Description, src => src.Description);
    }
}