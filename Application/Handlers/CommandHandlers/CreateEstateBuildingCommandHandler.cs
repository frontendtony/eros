using Api.ResponseModels;
using Application.Exceptions;
using EstateManager.Commands;
using EstateManager.Entities;
using EstateManager.Interfaces;
using FluentValidation;

namespace EstateManager.Handlers.CommandHandlers;

public class CreateEstateBuildingCommandHandler : EstateManagerBaseHandler
{
    private readonly IValidator<CreateEstateBuildingCommand> _validator;
    private readonly IEstateReadRepository _estateReadRepository;
    private readonly IEstateWriteRepository _estateWriteRepository;

    public CreateEstateBuildingCommandHandler(
        IEstateWriteRepository estateWriteRepository,
        IEstateReadRepository estateReadRepository,
        IValidator<CreateEstateBuildingCommand> validator,
        IHttpContextAccessor httpContextAccessor
        ) : base(httpContextAccessor)
    {
        _estateReadRepository = estateReadRepository;
        _estateWriteRepository = estateWriteRepository;
        _validator = validator;
    }

    public async Task<SingleResponseModel<EstateBuildingResponseModel>> Handle(CreateEstateBuildingCommand building, Guid estateId)
    {
        var validationResult = await _validator.ValidateAsync(building);

        if (!validationResult.IsValid)
        {
            throw new CustomValidationException(validationResult.Errors.Select(x => new ValidationFailure()
            {
                ErrorMessage = x.ErrorMessage
            })
            .ToList());
        }

        var estate = await _estateReadRepository.GetById(estateId) ?? throw new NotFoundException("Estate not found");
        var existingBuilding = await _estateReadRepository.GetEstateBuildingByName(building.Name, estateId);

        if (existingBuilding != null)
        {
            throw new ConflictException("Building with the same name already exists");
        }

        var newBuilding = new EstateBuilding
        {
            Id = Guid.NewGuid(),
            Name = building.Name,
            Description = building.Description,
            EstateId = estateId,
            CreatedBy = GetUserId(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        try
        {
            var eb = await _estateWriteRepository.CreateBuildingAsync(newBuilding);
            await _estateWriteRepository.SaveChangesAsync();

            return new SingleResponseModel<EstateBuildingResponseModel>
            {
                Data = new EstateBuildingResponseModel()
                {
                    Id = eb.Id,
                    Name = eb.Name,
                    EstateId = eb.EstateId,
                    EstateName = estate.Name,
                    Description = eb.Description,
                    CreatedAt = eb.CreatedAt,
                    UpdatedAt = eb.UpdatedAt,
                },
                StatusCode = StatusCodes.Status201Created
            };
        }
        catch (Exception ex)
        {
            throw new DatabaseException("Failed to create estate building", ex);
        }
    }
}
