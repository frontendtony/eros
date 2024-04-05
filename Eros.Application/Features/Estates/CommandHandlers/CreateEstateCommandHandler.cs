using Eros.Api.Dto.Estates;
using Eros.Application.Exceptions;
using Eros.Application.Features.Estates.Commands;
using Eros.Domain.Aggregates.Estates;
using Eros.Domain.Aggregates.Estates.Repositories;
using ErrorOr;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Eros.Application.Features.Estates.CommandHandlers;

public class CreateEstateCommandHandler(
    IEstateWriteRepository estateWriteRepository,
    IEstateReadRepository estateReadRepository,
    ILogger<CreateEstateCommandHandler> logger
) : IRequestHandler<CreateEstateCommand, ErrorOr<CreateEstateCommandDto>>
{
    public async Task<ErrorOr<CreateEstateCommandDto>> Handle(CreateEstateCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating estate with name {Name}", command.Name);
        
        logger.LogInformation("Checking if estate with the same name already exists");
        var existingEstate = await estateReadRepository.GetByNameAsync(command.Name, cancellationToken);

        if (existingEstate != null)
        {
            logger.LogWarning("Estate with the same name already exists. Name: {Name}", command.Name);
            throw new ConflictException($"Estate with the same name already exists. Name: {command.Name}");
        }
        
        var estate = new Estate(command.Name, command.Address, command.UserId);

        await estateWriteRepository.AddAsync(estate, cancellationToken);
        logger.LogInformation("Estate created with name {Name}", command.Name);
        
        return estate.Adapt<CreateEstateCommandDto>();
    }
}