using Eros.Api.Attributes;
using Eros.Api.Dto.ApiResponseModels;
using Eros.Api.Dto.Estates;
using Eros.Application.Features.Estates.Commands;
using Eros.Application.Features.Estates.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eros.Api.Controllers.EstateManager.Estates;

[ApiController]
[Route("api/estates")]
[Authorize]
[ForbidAdmin]
public class EstatesController : EstateManagerControllerBase
{
  [HttpGet]
  [ProducesResponseType(typeof(EstateDto), StatusCodes.Status200OK)]
  public async Task<ActionResult<SingleResponseModel<EstateDto>>> GetEstates()
  {
    var response = await Mediator.Send(new GetUserEstatesQuery(UserId));

    return Ok(
      new SingleResponseModel<List<EstateDto>>
      {
        Data = response
      }
    );
  }

  [HttpPost(Name = "CreateEstate")]
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
      new SingleResponseModel<CreateEstateCommandDto>
      {
        Data = estate
      }
    );
  }

  [HttpDelete("{id}", Name = "DeleteEstate")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult> DeleteEstate(Guid id)
  {
    await Mediator.Send(new DeleteEstateCommand(id));

    return NoContent();
  }
}
