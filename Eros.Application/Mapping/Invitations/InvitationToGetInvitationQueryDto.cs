using Eros.Api.Dto.Invitations;
using Eros.Domain.Aggregates.Invitations;
using Mapster;

namespace Eros.Application.Mapping.Invitations;

public class InvitationToGetInvitationQueryDto : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Invitation, GetInvitationQueryDto>()
            .Map(dest => dest.Status, src => src.Status.ToString());
    }
}
