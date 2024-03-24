using Eros.Application.Exceptions;
using Eros.Application.Features.Estates.Commands;
using Eros.Application.Features.Estates.Models;
using Eros.Domain.Aggregates.Buildings;
using Eros.Domain.Aggregates.Estates;
using MediatR;

namespace Eros.Application.Features.Estates.CommandHandlers;

public class CreateEstateBuildingCommandHandler : IRequestHandler<CreateEstateBuildingCommand, EstateBuildingResponseModel>
{
    private readonly IBuildingWriteRepository _buildingWriteRepository;
    private readonly IBuildingReadRepository _buildingReadRepository;
    private readonly IEstateReadRepository _estateReadRepository;
    private readonly IBuildingTypeReadRepository _buildingTypeReadRepository;

    public CreateEstateBuildingCommandHandler(
        IBuildingWriteRepository buildingWriteRepository,
        IBuildingReadRepository buildingReadRepository,
        IEstateReadRepository estateReadRepository,
        IBuildingTypeReadRepository buildingTypeReadRepository
    )
    {
        _buildingWriteRepository = buildingWriteRepository;
        _buildingReadRepository = buildingReadRepository;
        _estateReadRepository = estateReadRepository;
        _buildingTypeReadRepository = buildingTypeReadRepository;
    }

    public async Task<EstateBuildingResponseModel> Handle(CreateEstateBuildingCommand request, CancellationToken cancellationToken)
    {
        var existingBuilding = await _buildingReadRepository.GetByNameAsync(request.Name, cancellationToken);

        if (existingBuilding != null)
        {
            throw new ConflictException($"Building with the same name already exists. BuildingName: {request.Name}");
        }

        var _ = await _estateReadRepository.GetByIdAsync(request.EstateId, cancellationToken)
            ?? throw new NotFoundException($"Estate not found. EstateId: {request.EstateId}");

        var buildingType = await _buildingTypeReadRepository.GetByIdAsync(request.BuildingTypeId, cancellationToken)
            ?? throw new NotFoundException($"Invalid building type. BuildingTypeId: {request.BuildingTypeId}");

        var building = new Building()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Address = request.Address,
            EstateId = request.EstateId,
            BuildingTypeId = request.BuildingTypeId,
            CreatedAt = DateTime.UtcNow
        };

        await _buildingWriteRepository.AddAsync(building, cancellationToken);

        return new EstateBuildingResponseModel(
            building.Id,
            building.Name,
            building.Description,
            building.Address,
            building.EstateId,
            buildingType.Name,
            building.CreatedAt,
            building.UpdatedAt
        );
    }
}