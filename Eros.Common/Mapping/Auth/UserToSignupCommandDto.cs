using Eros.Api.Dto.Auth;
using Eros.Domain.Aggregates.Users;
using Mapster;

namespace Eros.Common.Mapping.Auth;

public class UserToSignupCommandDto : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<User, SignupCommandDto>()
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.FirstName, src => src.FirstName)
            .Map(dest => dest.LastName, src => src.LastName);
    }
}