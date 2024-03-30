using Eros.Application.Exceptions;
using Eros.Application.Features.Estates.Models;
using Eros.Application.Features.Estates.Queries;
using Eros.Domain.Aggregates.Estates;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Eros.Application.Features.Estates.QueryHandlers;

public class QueryEstateQueryHandler(
    IHttpContextAccessor httpContextAccessor,
    IEstateReadRepository estateReadRepository)
    : ErosBaseHandler(httpContextAccessor), IRequestHandler<GetEstateQuery, EstateResponseModel>
{
    public async Task<EstateResponseModel> Handle(GetEstateQuery query, CancellationToken cancellationToken)
    {
        var estate = await estateReadRepository.GetByIdAsync(query.Id, cancellationToken) ?? throw new NotFoundException("Estate not found");

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