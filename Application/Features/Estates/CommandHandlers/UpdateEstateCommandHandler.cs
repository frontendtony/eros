using Eros.Application.Exceptions;
using Eros.Application.Features.Estates.Commands;
using Eros.Application.Features.Estates.Models;
using Eros.Domain.Aggregates.Estates;
using Eros.Domain.Aggregates.Estates.Repositories;
using MediatR;

namespace Eros.Application.Features.Estates.CommandHandlers;

public class UpdateEstateCommandHandler : IRequestHandler<UpdateEstateCommand, EstateResponseModel>
{
    private readonly IEstateWriteRepository _estateWriteRepository;
    private readonly IEstateReadRepository _estateReadRepository;

    public UpdateEstateCommandHandler(IEstateWriteRepository estateWriteRepository, IEstateReadRepository estateReadRepository)
    {
        _estateWriteRepository = estateWriteRepository;
        _estateReadRepository = estateReadRepository;
    }

    public async Task<EstateResponseModel> Handle(UpdateEstateCommand request, CancellationToken cancellationToken)
    {
        var estate = await _estateReadRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException("Estate not found");

        estate.Address = request.Address ?? estate.Address;
        estate.Name = request.Name ?? estate.Name;
        estate.LatLng = request.LatLng ?? estate.LatLng;
        estate.UpdatedAt = DateTime.UtcNow;

        return await _estateWriteRepository.UpdateAsync(estate, cancellationToken)
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
