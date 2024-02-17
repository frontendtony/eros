using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EstateManager.Handlers.QueryHandlers;
using EstateManager.Handlers.CommandHandlers;
using MediatR;
using Eros.Application.Features.Estates.Queries;
using Eros.Api.Models;
using Eros.Application.Features.Estates.Models;
using Eros.Application.Features.Estates.Commands;
using Eros.Domain.Aggregates.Estates;

namespace Eros.Controllers;

[ApiController]
[Route("api/estates")]
[Authorize]
public class EstatesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly GetEstateQueryHandler _getEstateQueryHandler;
    private readonly CreateEstateCommandHandler _createEstateCommandHandler;
    private readonly UpdateEstateCommandHandler _updateEstateCommandHandler;
    private readonly DeleteEstateCommandHandler _deleteEstateCommandHandler;

    public EstatesController(
        IMediator mediator,
        GetEstateQueryHandler estateQueryHandler,
        CreateEstateCommandHandler createEstateCommandHandler,
        UpdateEstateCommandHandler updateEstateCommandHandler,
        DeleteEstateCommandHandler deleteEstateCommandHandler
    )
    {
        _mediator = mediator;
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
        var response = await _mediator.Send(new GetEstateByIdQuery(id));

        return Ok(
            new SingleResponseModel<EstateResponseModel>()
            {
                Data = response
            }
        );
    }

    [HttpPost(Name = "CreateEstate")]
    [ProducesResponseType(typeof(EstateResponseModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<SingleResponseModel<EstateResponseModel>>> CreateEstate([FromBody] CreateEstateCommand command)
    {
        var estate = await _mediator.Send(command);

        return Ok(
            new SingleResponseModel<EstateResponseModel>()
            {
                Data = estate
            }
        );
    }

    [HttpPut("{id}", Name = "UpdateEstate")]
    [ProducesResponseType(typeof(EstateResponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SingleResponseModel<EstateResponseModel>>> UpdateEstate(Guid id, [FromBody] Estate estate)
    {
        var response = await _mediator.Send(new UpdateEstateCommand(id, estate.Name, estate.Address, estate.LatLng));

        return Ok(
            new SingleResponseModel<EstateResponseModel>()
            {
                Data = response
            }
        );
    }

    [HttpDelete("{id}", Name = "DeleteEstate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SingleResponseModel<EstateResponseModel>>> DeleteEstate(Guid id)
    {
        await _mediator.Send(new DeleteEstateCommand(id));

        return NoContent();
    }
}