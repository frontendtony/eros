using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.ResponseModels;
using EstateManager.Commands;
using EstateManager.Handlers.QueryHandlers;
using EstateManager.Handlers.CommandHandlers;
using Eros.Api.Models;

namespace EstateManager.Controllers;

[ApiController]
[Authorize]
[Route("/api/estates/{id:guid}/buildings")]
public class EstateBuildingsController : ControllerBase
{
    private readonly GetEstateBuildingsQueryHandler _getEstateBuildingsQueryHandler;
    private readonly CreateEstateBuildingCommandHandler _createEstateBuildingCommandHandler;
    private readonly UpdateEstateBuildingCommandHandler _updateEstateBuildingCommandHandler;

    public EstateBuildingsController(
        GetEstateBuildingsQueryHandler getEstateBuildingsQueryHandler,
        CreateEstateBuildingCommandHandler createEstateBuildingCommandHandler,
        UpdateEstateBuildingCommandHandler updateEstateBuildingCommandHandler
    )
    {
        _getEstateBuildingsQueryHandler = getEstateBuildingsQueryHandler;
        _createEstateBuildingCommandHandler = createEstateBuildingCommandHandler;
        _updateEstateBuildingCommandHandler = updateEstateBuildingCommandHandler;
    }

    [HttpPost(Name = "CreateEstateBuilding")]
    [ProducesResponseType(typeof(EstateBuildingResponseModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateEstateBuilding(Guid id, [FromBody] CreateEstateBuildingCommand command)
    {
        return Ok(await _createEstateBuildingCommandHandler.Handle(command, id));
    }

    [HttpPut("{buildingId:guid}", Name = "UpdateEstateBuilding")]
    [ProducesResponseType(typeof(EstateBuildingResponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateEstateBuilding(Guid id, Guid buildingId, [FromBody] UpdateEstateBuildingCommand command)
    {
        return Ok(await _updateEstateBuildingCommandHandler.Handle(command, buildingId, id));
    }

    [HttpGet(Name = "GetEstateBuildings")]
    [ProducesResponseType(typeof(List<EstateBuildingResponseModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PaginatedResponseModel<EstateBuildingResponseModel>>> GetEstateBuildings(Guid id, [FromQuery] PaginationQueryModel paginationQuery)
    {
        return Ok(await _getEstateBuildingsQueryHandler.Handle(id, paginationQuery));
    }
}