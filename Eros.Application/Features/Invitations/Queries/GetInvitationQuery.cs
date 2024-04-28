using Eros.Api.Dto.Invitations;
using MediatR;

namespace Eros.Application.Features.Invitations.Queries;

public sealed record GetInvitationQuery(string Code) : IRequest<GetInvitationQueryDto>;
