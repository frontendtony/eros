using Eros.Api.Dto.Invitations;
using Eros.Application.Features.Invitations.Commands;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eros.Api.Controllers.EstateManager.Invitations;

[Route("api/invitations")]
[Authorize]
public class InvitationsController : EstateManagerControllerBase
{
    [HttpPost]
    public async Task<IActionResult> SendInvitationsAsync(SendInvitationsDto request)
    {
        var command = request.Adapt<SendInvitationsCommand>();
        command.SenderId = UserId;

        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> GetInvitationAsync(string code)
    {
        return Ok(code);
    }
}
