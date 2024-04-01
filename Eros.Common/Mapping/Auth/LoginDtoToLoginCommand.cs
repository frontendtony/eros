using Eros.Api.Dto.Auth;
using Eros.Application.Features.Auth.Commands;
using Mapster;

namespace Eros.Common.Mapping.Auth;

public class LoginDtoToLoginCommand : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<LoginDto, LoginCommand>()
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.Password, src => src.Password);
    }
}