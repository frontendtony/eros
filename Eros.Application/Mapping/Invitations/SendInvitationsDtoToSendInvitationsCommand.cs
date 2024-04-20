using Eros.Api.Dto.Invitations;
using Eros.Application.Features.Invitations.Commands;
using Mapster;

namespace Eros.Application.Mapping.Invitations;

public class SendInvitationsDtoToSendInvitationsCommand : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SendInvitationsDto, SendInvitationsCommand>()
            .Map(dest => dest.Emails, src => src.Emails)
            .Map(dest => dest.RoleId, src => Guid.Parse(src.RoleId))
            .Map(dest => dest.EstateId, src => Guid.Parse(src.EstateId));
    }
}