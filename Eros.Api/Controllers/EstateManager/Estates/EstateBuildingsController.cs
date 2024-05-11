using Eros.Api.Contracts.Buildings;
using Eros.Api.Dto.ApiResponseModels;
using Eros.Application.Features.Estates.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eros.Api.Controllers.EstateManager.Estates;

[ApiController]
[Authorize]
[Route("/api/estates/{estateId:guid}/buildings")]
public class EstateBuildingsController(ISender mediator) : ControllerBase
{
    [HttpPost(Name = "CreateEstateBuilding")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SingleResponseModel<BuildingResponse>>> CreateEstateBuilding(Guid estateId,
        [FromBody] CreateBuildingRequest request)
    {
        var response = await mediator.Send(new CreateEstateBuildingCommand(
            request.Name,
            request.Description,
            request.Address,
            estateId,
            Guid.Parse(request.BuildingTypeId)
        ));

        // return SingleResponseModel with BuildingResponse and 201 Created status code
        return CreatedAtRoute("CreateEstateBuilding", new { estateId, buildingId = response.Id },
            new SingleResponseModel<BuildingResponse>
            {
                Data = new BuildingResponse(
                    response.Id,
                    response.Name,
                    response.Description,
                    response.Address,
                    response.BuildingType,
                    response.EstateId,
                    response.CreatedAt,
                    response.UpdatedAt
                )
            });
    }

    [HttpPut("{buildingId:guid}", Name = "UpdateEstateBuilding")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SingleResponseModel<BuildingResponse>>> UpdateEstateBuilding(Guid estateId,
        Guid buildingId, [FromBody] UpdateBuildingRequest request)
    {
        var response = await mediator.Send(new UpdateEstateBuildingCommand(
            buildingId,
            estateId,
            request.Name,
            request.Description,
            request.Address,
            Guid.Parse(request.BuildingTypeId)
        ));

        return Ok(new SingleResponseModel<BuildingResponse>
        {
            Data = new BuildingResponse(
                response.Id,
                response.Name,
                response.Description,
                response.Address,
                response.BuildingType,
                response.EstateId,
                response.CreatedAt,
                response.UpdatedAt
            )
        });
    }
}
