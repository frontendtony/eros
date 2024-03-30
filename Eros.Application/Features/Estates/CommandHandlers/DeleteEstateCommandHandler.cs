using Eros.Application.Exceptions;
using Eros.Application.Features.Estates.Commands;
using Eros.Domain.Aggregates.Estates;
using Eros.Domain.Aggregates.Estates.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Eros.Application.Features.Estates.CommandHandlers;

public class DeleteEstateCommandHandler(
    IHttpContextAccessor httpContextAccessor,
    IEstateReadRepository estateReadRepository,
    IEstateWriteRepository estateWriteRepository)
    : ErosBaseHandler(httpContextAccessor), IRequestHandler<DeleteEstateCommand>
{
    public async Task Handle(DeleteEstateCommand request, CancellationToken cancellationToken)
    {
        var estate = await estateReadRepository.GetByIdAsync(request.Id, cancellationToken) ?? throw new NotFoundException("Estate not found");

        await estateWriteRepository.DeleteAsync(estate, cancellationToken);
    }
}