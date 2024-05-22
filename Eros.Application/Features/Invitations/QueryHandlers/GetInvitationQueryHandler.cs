using Eros.Api.Dto.Invitations;
using Eros.Application.Exceptions;
using Eros.Application.Features.Invitations.Queries;
using Eros.Domain.Aggregates.Estates;
using Eros.Domain.Aggregates.Invitations;
using Eros.Domain.Aggregates.Roles;
using Eros.Domain.Aggregates.Users;
using Mapster;
using MediatR;

namespace Eros.Application.Features.Invitations.QueryHandlers;

public class GetInvitationQueryHandler(
  IInvitationReadRepository invitationRepository,
  IRoleReadRepository roleRepository,
  IEstateReadRepository estateRepository,
  IUserReadRepository userReadRepository
) : IRequestHandler<GetInvitationQuery, GetInvitationQueryDto>
{
  public async Task<GetInvitationQueryDto> Handle(GetInvitationQuery request, CancellationToken cancellationToken)
  {
    var invitation = await invitationRepository.GetByCodeAsync(request.Code, cancellationToken)
                     ?? throw new NotFoundException("Invitation not found");

    var estate = await estateRepository.GetByIdAsync(invitation.EstateId, cancellationToken)
                 ?? throw new InconsistentDataException("Estate not found");

    var role = await roleRepository.GetByIdAsync(invitation.RoleId)
               ?? throw new InconsistentDataException("Role not found");

    var inviter = await userReadRepository.GetByIdAsync(invitation.CreatedBy, cancellationToken)
                  ?? throw new InconsistentDataException("User not found");

    var getInvitationQueryDto = invitation.Adapt<GetInvitationQueryDto>();
    getInvitationQueryDto.EstateName = estate.Name;
    getInvitationQueryDto.RoleName = role.Name;
    getInvitationQueryDto.InviterName = inviter.Name;

    return getInvitationQueryDto;
  }
}
