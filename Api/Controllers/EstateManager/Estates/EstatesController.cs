using EstateManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Api.ResponseModels;
using EstateManager.Commands;
using EstateManager.Handlers.QueryHandlers;
using EstateManager.Handlers.CommandHandlers;

namespace EstateManager.Controllers;

[ApiController]
[Route("api/estates")]
[Authorize]
public class EstatesController : ControllerBase
{
    private readonly GetEstateQueryHandler _getEstateQueryHandler;
    private readonly CreateEstateCommandHandler _createEstateCommandHandler;
    private readonly UpdateEstateCommandHandler _updateEstateCommandHandler;
    private readonly DeleteEstateCommandHandler _deleteEstateCommandHandler;

    public EstatesController(
        GetEstateQueryHandler estateQueryHandler,
        CreateEstateCommandHandler createEstateCommandHandler,
        UpdateEstateCommandHandler updateEstateCommandHandler,
        DeleteEstateCommandHandler deleteEstateCommandHandler
    )
    {
        _getEstateQueryHandler = estateQueryHandler;
        _createEstateCommandHandler = createEstateCommandHandler;
        _updateEstateCommandHandler = updateEstateCommandHandler;
        _deleteEstateCommandHandler = deleteEstateCommandHandler;
    }

    [HttpGet("{id:guid}", Name = "GetEstate")]
    [ProducesResponseType(typeof(EstateResponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SingleResponseModel<EstateResponseModel>>> GetEstate(Guid id)
    {
        return Ok(await _getEstateQueryHandler.GetEstate(id));
    }

    [HttpPost(Name = "CreateEstate")]
    [ProducesResponseType(typeof(EstateResponseModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<SingleResponseModel<EstateResponseModel>>> CreateEstate([FromBody] CreateEstateCommand command)
    {
        var estate = await _createEstateCommandHandler.CreateEstate(command);
        return Created($"/api/estates/{estate.Data?.Id}", estate);
    }

    [HttpPut("{id}", Name = "UpdateEstate")]
    [ProducesResponseType(typeof(EstateResponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SingleResponseModel<EstateResponseModel>>> UpdateEstate(Guid id, [FromBody] UpdateEstateCommand command)
    {
        return Ok(await _updateEstateCommandHandler.UpdateEstate(command, id));
    }

    [HttpDelete("{id}", Name = "DeleteEstate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SingleResponseModel<EstateResponseModel>>> DeleteEstate(Guid id)
    {
        return Ok(await _deleteEstateCommandHandler.DeleteEstate(id));
    }
}