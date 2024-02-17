using Eros.Application.Exceptions;
using Eros.Application.Features.Estates.Commands;
using Eros.Domain.Aggregates.Estates;
using Eros.Domain.Aggregates.Estates.Repositories;
using Eros.Handlers;
using MediatR;

namespace Eros.Application.Features.Estates.CommandHandlers;

public class DeleteEstateCommandHandler : BaseHandler, IRequestHandler<DeleteEstateCommand>
{
    IEstateReadRepository _estateReadRepository;
    IEstateWriteRepository _estateWriteRepository;

    public DeleteEstateCommandHandler(
        HttpContextAccessor httpContextAccessor,
        IEstateReadRepository estateReadRepository,
        IEstateWriteRepository estateWriteRepository
    ) : base(httpContextAccessor)
    {
        _estateReadRepository = estateReadRepository;
        _estateWriteRepository = estateWriteRepository;
    }

    public async Task Handle(DeleteEstateCommand request, CancellationToken cancellationToken)
    {
        var estate = await _estateReadRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException("Estate not found");

        await _estateWriteRepository.DeleteAsync(estate, cancellationToken);
    }
}