using Api.ResponseModels;
using Application.Exceptions;
using EstateManager.Commands;
using EstateManager.Entities;
using EstateManager.Interfaces;
using FluentValidation;

namespace EstateManager.Handlers.CommandHandlers;

public class CreateEstateCommandHandler : EstateManagerBaseHandler
{
    private readonly IEstateWriteRepository _estateWriteRepository;
    private readonly IEstateReadRepository _estateReadRepository;
    private readonly IValidator<CreateEstateCommand> _validator;

    public CreateEstateCommandHandler(
        IEstateWriteRepository estateWriteRepository,
        IEstateReadRepository estateReadRepository,
        IHttpContextAccessor httpContextAccessor,
        IValidator<CreateEstateCommand> validator
    ) : base(httpContextAccessor)
    {
        _estateWriteRepository = estateWriteRepository;
        _estateReadRepository = estateReadRepository;
        _validator = validator;
    }

    public async Task<SingleResponseModel<EstateResponseModel>> CreateEstate(CreateEstateCommand command)
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

        var existingEstate = await _estateReadRepository.GetByName(command.Name);
        if (existingEstate is not null)
        {
            throw new ConflictException($"Estate with name '{command.Name}' already exists");
        }

        var newEstate = new Estate(command, base.GetUserId());

        try
        {
            var createdEstate = _estateWriteRepository.Create(newEstate);
            await _estateWriteRepository.SaveChangesAsync();

            return new SingleResponseModel<EstateResponseModel>
            {
                Data = new EstateResponseModel(createdEstate),
                StatusCode = StatusCodes.Status201Created
            };
        }
        catch (Exception ex)
        {
            throw new DatabaseException("Failed to create estate", ex);
        }
    }
}
