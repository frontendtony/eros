using Api.ResponseModels;
using Application.Exceptions;
using EstateManager.Interfaces;
using EstateManager.Constants;

namespace EstateManager.Handlers.QueryHandlers;

public class GetEstateBuildingsQueryHandler : EstateManagerBaseHandler
{
    protected IEstateReadRepository _estateReadRepository { get; }
    public GetEstateBuildingsQueryHandler(IHttpContextAccessor httpContextAccessor, IEstateReadRepository estateReadRepository) : base(httpContextAccessor)
    {
        _estateReadRepository = estateReadRepository;
    }

    public async Task<PaginatedResponseModel<EstateBuildingResponseModel>> Handle(Guid estateId, PaginationQueryModel paginationQuery)
    {
        var estate = await _estateReadRepository.GetById(estateId);
        if (estate == null)
        {
            throw new NotFoundException("Estate not found");
        }

        var pageSize = Math.Min(GlobalConstants.MaxPageSize, Math.Max(1, paginationQuery.PageSize)); // PageSize must be between 1 and MaxPageSize
        var pageNumber = Math.Max(1, paginationQuery.PageNumber); // PageNumber is 1-indexed

        var estateBuildings = await _estateReadRepository.GetEstateBuildings(estateId, pageSize, pageNumber);

        return new PaginatedResponseModel<EstateBuildingResponseModel>
        {
            Data = estateBuildings
                .Select(building => new EstateBuildingResponseModel()
                {
                    Id = building.Id,
                    Name = building.Name,
                    Description = building.Description,
                    EstateId = estateId,
                    EstateName = estate.Name,
                    CreatedBy = building.CreatedBy,
                    CreatedAt = building.CreatedAt,
                    UpdatedAt = building.UpdatedAt,
                }).ToList(),
            StatusCode = StatusCodes.Status200OK,
            PageSize = pageSize,
            PageNumber = pageNumber,
            Total = estate.Buildings.Count(),
        };
    }
}