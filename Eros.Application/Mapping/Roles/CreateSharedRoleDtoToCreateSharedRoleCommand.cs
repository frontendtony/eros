using Eros.Api.Dto.Roles;
using Eros.Application.Features.Roles.Commands;
using Mapster;

namespace Eros.Application.Mapping.Roles;

public class CreateSharedRoleDtoToCreateSharedRoleCommand : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateSharedRoleDto, CreateSharedRoleCommand>()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.PermissionIds, src =>
                src.PermissionIds.Select(Guid.Parse));
    }
}