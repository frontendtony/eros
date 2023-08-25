using Api.ResponseModels;
using Application.Exceptions;
using EstateManager.Commands;
using EstateManager.Interfaces;
using FluentValidation;

namespace EstateManager.Handlers.CommandHandlers;

public class UpdateEstateCommandHandler : EstateManagerBaseHandler
{
    private readonly IEstateWriteRepository _estateWriteRepository;
    private readonly IEstateReadRepository _estateReadRepository;
    private readonly IValidator<UpdateEstateCommand> _validator;

    public UpdateEstateCommandHandler(
        IEstateWriteRepository estateWriteRepository,
        IEstateReadRepository estateReadRepository,
        IHttpContextAccessor httpContextAccessor,
        IValidator<UpdateEstateCommand> validator
    ) : base(httpContextAccessor)
    {
        _estateWriteRepository = estateWriteRepository;
        _estateReadRepository = estateReadRepository;
        _validator = validator;
    }

    public async Task<SingleResponseModel<EstateResponseModel>> UpdateEstate(UpdateEstateCommand command, Guid estateId)
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

        var existingEstate = await _estateReadRepository.GetById(estateId) ?? throw new NotFoundException($"Estate with id '{estateId}' does not exist");

        existingEstate.Update(command);

        try
        {
            var updatedEstate = _estateWriteRepository.Update(existingEstate);
            await _estateWriteRepository.SaveChangesAsync();

            return new SingleResponseModel<EstateResponseModel>
            {
                Data = new EstateResponseModel(updatedEstate),
                StatusCode = StatusCodes.Status200OK
            };
        }
        catch (Exception ex)
        {
            throw new DatabaseException("Failed to update estate", ex);
        }
    }
}
