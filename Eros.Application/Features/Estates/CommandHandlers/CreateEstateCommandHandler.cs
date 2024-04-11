using Eros.Api.Dto.Estates;
using Eros.Application.Exceptions;
using Eros.Application.Features.Estates.Commands;
using Eros.Domain.Aggregates.Estates;
using Eros.Domain.Aggregates.Roles;
using Eros.Persistence;
using ErrorOr;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Eros.Application.Features.Estates.CommandHandlers;

public class CreateEstateCommandHandler(
    ErosDbContext dbContext,
    IEstateWriteRepository estateWriteRepository,
    IEstateReadRepository estateReadRepository,
    IEstateRoleWriteRepository estateRoleWriteRepository,
    IRoleReadRepository roleReadRepository,
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
        
        // add all the shared roles to the estate
        logger.LogInformation("Adding shared roles to estate");

        // fetch all the shared roles from the database
        var sharedRoles = await roleReadRepository.GetSharedRolesAsync(cancellationToken);
        
        foreach (var role in sharedRoles)
        {
            await estateRoleWriteRepository.AddAsync(new EstateRole()
            {
                RoleId = role.Id,
                EstateId = estate.Id
            }, cancellationToken);
        }

        await estateWriteRepository.AddAsync(estate, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Estate created with name {Name}", command.Name);
        
        return estate.Adapt<CreateEstateCommandDto>();
    }
}