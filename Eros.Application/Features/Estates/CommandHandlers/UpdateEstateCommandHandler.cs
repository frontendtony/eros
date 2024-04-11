using Eros.Application.Exceptions;
using Eros.Application.Features.Estates.Commands;
using Eros.Application.Features.Estates.Models;
using Eros.Domain.Aggregates.Estates;
using Eros.Persistence;
using MediatR;

namespace Eros.Application.Features.Estates.CommandHandlers;

public class UpdateEstateCommandHandler(
    ErosDbContext dbContext,
    IEstateWriteRepository estateWriteRepository,
    IEstateReadRepository estateReadRepository)
    : IRequestHandler<UpdateEstateCommand, EstateResponseModel>
{
    public async Task<EstateResponseModel> Handle(UpdateEstateCommand request, CancellationToken cancellationToken)
    {
        var estate = await estateReadRepository.GetByIdAsync(request.Id, cancellationToken) ?? throw new NotFoundException("Estate not found");

        estate.Address = request.Address ?? estate.Address;
        estate.Name = request.Name ?? estate.Name;
        estate.LatLng = request.LatLng ?? estate.LatLng;
        estate.UpdatedAt = DateTime.UtcNow;

        estateWriteRepository.UpdateAsync(estate, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

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
