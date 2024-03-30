using Eros.Application.Exceptions;
using Eros.Application.Features.Estates.Commands;
using Eros.Application.Features.Estates.Models;
using Eros.Domain.Aggregates.Buildings;
using Eros.Domain.Aggregates.Estates;
using MediatR;

namespace Eros.Application.Features.Estates.CommandHandlers;

public class UpdateEstateBuildingCommandHandler(
    IBuildingWriteRepository buildingWriteRepository,
    IBuildingReadRepository buildingReadRepository,
    IEstateReadRepository estateReadRepository,
    IBuildingTypeReadRepository buildingTypeReadRepository)
    : IRequestHandler<UpdateEstateBuildingCommand, EstateBuildingResponseModel>
{
    public async Task<EstateBuildingResponseModel> Handle(UpdateEstateBuildingCommand request, CancellationToken cancellationToken)
    {
        _ = await estateReadRepository.GetByIdAsync(request.EstateId, cancellationToken)
            ?? throw new NotFoundException($"Estate not found. EstateId: {request.EstateId}");

        var buildingType = await buildingTypeReadRepository.GetByIdAsync(request.BuildingTypeId, cancellationToken)
            ?? throw new BadRequestException($"Invalid building type. BuildingTypeId: {request.BuildingTypeId}");

        var building = await buildingReadRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException($"Building not found. BuildingId: {request.Id}");

        building.Update(
            request.Name,
            request.Description,
            request.Address,
            request.BuildingTypeId
        );

        await buildingWriteRepository.UpdateAsync(building, cancellationToken);

        return new EstateBuildingResponseModel(
            building.Id,
            building.Name,
            building.Description ?? string.Empty,
            building.Address,
            building.EstateId,
            buildingType.Name,
            building.CreatedAt,
            building.UpdatedAt
        );
    }
}