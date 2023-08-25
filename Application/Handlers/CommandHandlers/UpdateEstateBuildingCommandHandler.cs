using Api.ResponseModels;
using Application.Exceptions;
using EstateManager.Commands;
using EstateManager.Interfaces;
using FluentValidation;

namespace EstateManager.Handlers.CommandHandlers;

public class UpdateEstateBuildingCommandHandler : EstateManagerBaseHandler
{
    private readonly IValidator<UpdateEstateBuildingCommand> _validator;
    private readonly IEstateReadRepository _estateReadRepository;
    private readonly IEstateWriteRepository _estateWriteRepository;

    public UpdateEstateBuildingCommandHandler(
        IEstateWriteRepository estateWriteRepository,
        IEstateReadRepository estateReadRepository,
        IValidator<UpdateEstateBuildingCommand> validator,
        IHttpContextAccessor httpContextAccessor
        ) : base(httpContextAccessor)
    {
        _estateReadRepository = estateReadRepository;
        _estateWriteRepository = estateWriteRepository;
        _validator = validator;
    }

    public async Task<SingleResponseModel<EstateBuildingResponseModel>> Handle(UpdateEstateBuildingCommand command, Guid buildingId, Guid estateId)
    {
        var validationResult = await _validator.ValidateAsync(command);

        if (!validationResult.IsValid)
        {
            throw new CustomValidationException(validationResult.Errors.Select(x => new ValidationFailure()
            {
                ErrorMessage = x.ErrorMessage
            })
            .ToList());
        }

        var estate = await _estateReadRepository.GetById(estateId) ?? throw new NotFoundException("Estate not found");
        var existingBuilding = await _estateReadRepository.GetEstateBuildingById(buildingId, estateId) ?? throw new NotFoundException("Building not found");

        var existingBuildingWithSameName = await _estateReadRepository.GetEstateBuildingByName(command.Name, estateId);
        if (existingBuildingWithSameName != null && existingBuildingWithSameName.Id != buildingId)
        {
            throw new ConflictException("Building with the same name already exists");
        }

        // only update the name if it's not null or empty
        existingBuilding.Name = !string.IsNullOrEmpty(command.Name) ? command.Name : existingBuilding.Name;
        existingBuilding.Description = command.Description ?? existingBuilding.Description;

        try
        {
            await _estateWriteRepository.SaveChangesAsync();

            return new SingleResponseModel<EstateBuildingResponseModel>
            {
                Data = new EstateBuildingResponseModel()
                {
                    Id = existingBuilding.Id,
                    Name = existingBuilding.Name,
                    EstateId = existingBuilding.EstateId,
                    EstateName = estate.Name,
                    Description = existingBuilding.Description,
                    CreatedAt = existingBuilding.CreatedAt,
                    UpdatedAt = existingBuilding.UpdatedAt,
                },
                StatusCode = StatusCodes.Status200OK
            };
        }
        catch (Exception ex)
        {
            throw new DatabaseException("Failed to update estate building", ex);
        }
    }
}
