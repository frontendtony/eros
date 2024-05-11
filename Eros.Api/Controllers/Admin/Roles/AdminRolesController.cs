using Eros.Api.Dto.ApiResponseModels;
using Eros.Api.Dto.Roles;
using Eros.Application.Features.Roles.Commands;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Eros.Api.Controllers.Admin.Roles;

[Route("/api/admin/roles")]
public class AdminRolesController : AdminControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateRole(CreateSharedRoleDto dto)
    {
        var createRoleCommand = dto.Adapt<CreateSharedRoleCommand>();
        var createRoleCommandResponse = await Mediator.Send(createRoleCommand);

        return Ok(
            new SingleResponseModel<CreateSharedRoleCommandDto>
            {
                Data = createRoleCommandResponse,
                Message = "Role created successfully"
            }
        );
    }
}
