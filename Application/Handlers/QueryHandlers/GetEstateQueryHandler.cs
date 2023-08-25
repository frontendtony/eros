using Api.ResponseModels;
using Application.Exceptions;
using EstateManager.Interfaces;

namespace EstateManager.Handlers.QueryHandlers;

public class GetEstateQueryHandler : EstateManagerBaseHandler
{
    protected IEstateReadRepository _estateReadRepository { get; }
    public GetEstateQueryHandler(IHttpContextAccessor httpContextAccessor, IEstateReadRepository estateReadRepository) : base(httpContextAccessor)
    {
        _estateReadRepository = estateReadRepository;
    }

    public async Task<SingleResponseModel<EstateResponseModel>> GetEstate(Guid estateId)
    {
        var estate = await _estateReadRepository.GetById(estateId);
        if (estate == null)
        {
            throw new NotFoundException("Estate not found");
        }

        return new SingleResponseModel<EstateResponseModel>
        {
            Data = new EstateResponseModel(estate),
            StatusCode = StatusCodes.Status200OK
        };
    }
}