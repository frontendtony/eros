using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using Eros.Application.Features.Estates.Queries;
using Eros.Api.Models;
using Eros.Application.Features.Estates.Commands;
using Eros.Domain.Aggregates.Estates;
using Eros.Contracts.Estates;

namespace Eros.Controllers;

[ApiController]
[Route("api/estates")]
[Authorize]
public class EstatesController : ControllerBase
{
    private readonly IMediator _mediator;

    public EstatesController(
        IMediator mediator
    )
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}", Name = "GetEstate")]
    [ProducesResponseType(typeof(EstateResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SingleResponseModel<EstateResponse>>> GetEstate(Guid id)
    {
        var response = await _mediator.Send(new GetEstateByIdQuery(id));

        return Ok(
            new SingleResponseModel<EstateResponse>()
            {
                Data = new EstateResponse(
                    response.Id,
                    response.Name,
                    response.Address,
                    response.LatLng,
                    response.CreatedBy,
                    response.CreatedAt,
                    response.UpdatedAt
                )
            }
        );
    }

    [HttpPost(Name = "CreateEstate")]
    [ProducesResponseType(typeof(EstateResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<SingleResponseModel<EstateResponse>>> CreateEstate([FromBody] CreateEstateCommand command)
    {
        var estate = await _mediator.Send(command);

        return Ok(
            new SingleResponseModel<EstateResponse>()
            {
                Data = new EstateResponse(
                    estate.Id,
                    estate.Name,
                    estate.Address,
                    estate.LatLng,
                    estate.CreatedBy,
                    estate.CreatedAt,
                    estate.UpdatedAt
                )
            }
        );
    }

    [HttpPut("{id}", Name = "UpdateEstate")]
    [ProducesResponseType(typeof(EstateResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SingleResponseModel<EstateResponse>>> UpdateEstate(Guid id, [FromBody] Estate estate)
    {
        var response = await _mediator.Send(new UpdateEstateCommand(id, estate.Name, estate.Address, estate.LatLng));

        return Ok(
            new SingleResponseModel<EstateResponse>()
            {
                Data = new EstateResponse(
                    response.Id,
                    response.Name,
                    response.Address,
                    response.LatLng,
                    response.CreatedBy,
                    response.CreatedAt,
                    response.UpdatedAt
                )
            }
        );
    }

    [HttpDelete("{id}", Name = "DeleteEstate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SingleResponseModel<EstateResponse>>> DeleteEstate(Guid id)
    {
        await _mediator.Send(new DeleteEstateCommand(id));

        return NoContent();
    }
}