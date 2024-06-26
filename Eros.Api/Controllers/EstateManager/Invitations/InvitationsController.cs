using Eros.Api.Dto.ApiResponseModels;
using Eros.Api.Dto.Invitations;
using Eros.Application.Features.Invitations.Commands;
using Eros.Application.Features.Invitations.Queries;
using Eros.Domain.Aggregates.Invitations;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eros.Api.Controllers.EstateManager.Invitations;

[Route("api/invitations")]
public class InvitationsController : EstateManagerControllerBase
{
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<SingleResponseModel<bool>>> SendInvitationsAsync(SendInvitationsDto request)
    {
        var command = request.Adapt<SendInvitationsCommand>();
        command.SenderId = UserId;

        var result = await Mediator.Send(command);

        return Ok(
            new SingleResponseModel<bool>
            {
                Data = result
            }
        );
    }

    [HttpGet("{code}")]
    public async Task<ActionResult<SingleResponseModel<GetInvitationQueryDto>>> GetInvitationAsync(string code)
    {
        var query = new GetInvitationQuery(code);
        var invitation = await Mediator.Send(query);

        return Ok(
            new SingleResponseModel<GetInvitationQueryDto>
            {
                Data = invitation
            }
        );
    }

    [HttpPatch("{code}")]
    [Authorize]
    public async Task<ActionResult<SingleResponseModel<AcceptInvitationCommandDto>>> AcceptInvitationAsync(string code,
        AcceptInvitationDto request)
    {
        var status = Enum.TryParse(request.Status, true, out InvitationStatus invitationStatus)
            ? invitationStatus
            : throw new ArgumentException("Invalid status. Accepted values are 'Accepted' or 'Rejected'");
        var command = new AcceptInvitationCommand(code, status, UserId, UserEmail);

        var result = await Mediator.Send(command);

        return Ok(
            new SingleResponseModel<AcceptInvitationCommandDto>
            {
                Data = result
            }
        );
    }
}
