using Eros.Api.Attributes;
using Eros.Api.Contracts.Estates;
using Eros.Api.Dto.Estates;
using Eros.Api.Models;
using Eros.Application.Features.Estates.Commands;
using Eros.Application.Features.Estates.Queries;
using Eros.Domain.Aggregates.Estates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eros.Api.Controllers.EstateManager.Estates;

[ApiController]
[Route("api/estates")]
[Authorize]
[ForbidAdmin]
public class EstatesController : ApiControllerBase
{
    [HttpGet("{id:guid}", Name = "GetEstate")]
    [ProducesResponseType(typeof(EstateResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SingleResponseModel<EstateResponse>>> GetEstate(Guid id)
    {
        var response = await Mediator.Send(new GetEstateQuery(id));

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
    public async Task<ActionResult<SingleResponseModel<CreateEstateCommandDto>>> CreateEstate(CreateEstateDto dto)
    {
        var command = new CreateEstateCommand(
            dto.Name,
            dto.Address,
            UserId
        );

        var estate = await Mediator.Send(command);

        return Created(
            Url.Link("GetEstate", new { id = estate.Id }),
            new SingleResponseModel<CreateEstateCommandDto>()
            {
                Data = estate,
            }
        );
    }

    [HttpPut("{id}", Name = "UpdateEstate")]
    [ProducesResponseType(typeof(EstateResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SingleResponseModel<EstateResponse>>> UpdateEstate(Guid id, [FromBody] Estate estate)
    {
        var response = await Mediator.Send(new UpdateEstateCommand(id, estate.Name, estate.Address, estate.LatLng));

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
        await Mediator.Send(new DeleteEstateCommand(id));

        return NoContent();
    }
}