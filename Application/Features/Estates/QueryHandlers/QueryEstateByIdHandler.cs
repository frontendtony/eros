using Eros.Application.Exceptions;
using Eros.Application.Features.Estates.Models;
using Eros.Application.Features.Estates.Queries;
using Eros.Domain.Aggregates.Estates;
using Eros.Handlers;
using MediatR;

namespace Eros.Application.Features.Estates.QueryHandlers;

public class QueryEstateByIdHandler : EstateManagerBaseHandler, IRequestHandler<GetEstateByIdQuery, EstateResponseModel>
{
    private readonly IEstateReadRepository _estateReadRepository;

    public QueryEstateByIdHandler(
        IHttpContextAccessor httpContextAccessor,
        IEstateReadRepository estateReadRepository
    ) : base(httpContextAccessor)
    {
        _estateReadRepository = estateReadRepository;
    }

    public async Task<EstateResponseModel> Handle(GetEstateByIdQuery query, CancellationToken cancellationToken)
    {
        var estate = await _estateReadRepository.GetByIdAsync(query.Id) ?? throw new NotFoundException("Estate not found");

        return new EstateResponseModel(
            estate.Id,
            estate.Name,
            estate.Address,
            estate.LatLng,
            estate.CreatedBy,
            estate.CreatedAt,
            estate.UpdatedAt
        );
    }
}