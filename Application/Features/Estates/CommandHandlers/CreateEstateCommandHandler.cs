using Eros.Application.Exceptions;
using Eros.Application.Features.Estates.Commands;
using Eros.Application.Features.Estates.Models;
using Eros.Domain.Aggregates.Estates;
using Eros.Domain.Aggregates.Estates.Repositories;
using Eros.Handlers;
using MediatR;

namespace Eros.Application.Features.Estates.CommandHandlers;

public class CreateEstateCommandHandler : EstateManagerBaseHandler, IRequestHandler<CreateEstateCommand, EstateResponseModel>
{
    private readonly IEstateWriteRepository _estateWriteRepository;
    private readonly IEstateReadRepository _estateReadRepository;

    public CreateEstateCommandHandler(
        IHttpContextAccessor httpContextAccessor,
        IEstateWriteRepository estateWriteRepository,
        IEstateReadRepository estateReadRepository
    ) : base(httpContextAccessor)
    {
        _estateWriteRepository = estateWriteRepository;
        _estateReadRepository = estateReadRepository;
    }

    public Task<EstateResponseModel> Handle(CreateEstateCommand request, CancellationToken cancellationToken)
    {
        var existingEstate = _estateReadRepository.GetByNameAsync(request.Name);

        if (existingEstate != null)
        {
            throw new ConflictException("Estate with the same name already exists");
        }

        var userId = GetUserId();
        var estate = new Estate(request.Name, request.Address, userId, request.LatLng);

        return _estateWriteRepository.AddAsync(estate, cancellationToken)
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