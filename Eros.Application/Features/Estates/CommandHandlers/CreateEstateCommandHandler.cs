using Eros.Application.Exceptions;
using Eros.Application.Features.Estates.Commands;
using Eros.Application.Features.Estates.Models;
using Eros.Domain.Aggregates.Estates;
using Eros.Domain.Aggregates.Estates.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Eros.Application.Features.Estates.CommandHandlers;

public class CreateEstateCommandHandler(
    IHttpContextAccessor httpContextAccessor,
    IEstateWriteRepository estateWriteRepository,
    IEstateReadRepository estateReadRepository)
    : ErosBaseHandler(httpContextAccessor), IRequestHandler<CreateEstateCommand, EstateResponseModel>
{
    public Task<EstateResponseModel> Handle(CreateEstateCommand request, CancellationToken cancellationToken)
    {
        var existingEstate = estateReadRepository.GetByNameAsync(request.Name, cancellationToken);

        if (existingEstate != null)
        {
            throw new ConflictException("Estate with the same name already exists");
        }

        var userId = GetUserId();
        var estate = new Estate(request.Name, request.Address, userId, request.LatLng);

        return estateWriteRepository.AddAsync(estate, cancellationToken)
            .ContinueWith(t => new EstateResponseModel(
                t.Result.Id,
                t.Result.Name,
                t.Result.Address,
                t.Result.LatLng,
                t.Result.CreatedBy,
                t.Result.CreatedAt,
                t.Result.UpdatedAt
            ), cancellationToken);
    }
}