using Eros.Api.Dto.Auth;
using Eros.Domain.Aggregates.Users;
using Mapster;

namespace Eros.Application.Mapping.Auth;

public class UserToUserDto : IRegister
{
  public void Register(TypeAdapterConfig config)
  {
    config.ForType<User, UserDto>()
      .Map(dest => dest.Id, src => src.Id.ToString());
  }
}
