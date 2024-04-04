using Eros.Api.Dto.Auth;
using Eros.Application.Features.Auth.Commands;
using Mapster;

namespace Eros.Application.Mapping.Auth;

public class SignupDtoToSignupCommand : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<SignupDto, SignupCommand>()
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.Password, src => src.Password)
            .Map(dest => dest.FirstName, src => src.FirstName)
            .Map(dest => dest.LastName, src => src.LastName);
    }
}