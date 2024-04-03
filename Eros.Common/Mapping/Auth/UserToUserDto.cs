using Eros.Api.Dto.Auth;
using Eros.Domain.Aggregates.Users;
using Mapster;

namespace Eros.Common.Mapping.Auth;

public class UserToUserDto : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<User, UserDto>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.FirstName, src => src.FirstName)
            .Map(dest => dest.LastName, src => src.LastName)
            .Map(dest => dest.IsAdmin, src => src.IsAdmin);
    }
}