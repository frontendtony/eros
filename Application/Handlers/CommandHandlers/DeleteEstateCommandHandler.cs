using Api.ResponseModels;
using Application.Exceptions;
using EstateManager.Interfaces;

namespace EstateManager.Handlers.CommandHandlers;

public class DeleteEstateCommandHandler
{
    private readonly IEstateWriteRepository _estateWriteRepository;
    private readonly IEstateReadRepository _estateReadRepository;

    public DeleteEstateCommandHandler(IEstateWriteRepository estateWriteRepository, IEstateReadRepository estateReadRepository)
    {
        _estateWriteRepository = estateWriteRepository;
        _estateReadRepository = estateReadRepository;
    }

    public async Task<SingleResponseModel<EstateResponseModel>> DeleteEstate(Guid estateId)
    {
        var estate = await _estateReadRepository.GetById(estateId);

        if (estate is null)
        {
            throw new NotFoundException($"Estate with id '{estateId}' does not exist");
        }

        try
        {
            _estateWriteRepository.Delete(estate);
            await _estateWriteRepository.SaveChangesAsync();

            return new SingleResponseModel<EstateResponseModel>
            {
                Data = new EstateResponseModel(estate),
                StatusCode = StatusCodes.Status204NoContent
            };
        }
        catch (Exception ex)
        {
            throw new DatabaseException("Failed to delete estate", ex);
        }
    }
}
