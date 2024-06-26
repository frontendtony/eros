using Eros.Api.Dto.Estates;
using Eros.Application.Exceptions;
using Eros.Application.Features.Estates.Commands;
using Eros.Domain.Aggregates.Estates;
using Eros.Domain.Aggregates.Roles;
using Eros.Domain.Aggregates.Users;
using Eros.Domain.Enums;
using Eros.Persistence;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Eros.Application.Features.Estates.CommandHandlers;

public class CreateEstateCommandHandler(
    ErosDbContext dbContext,
    IEstateWriteRepository estateWriteRepository,
    IEstateReadRepository estateReadRepository,
    IRoleReadRepository roleReadRepository,
    IUserReadRepository userReadRepository,
    IEstateUserWriteRepository estateUserWriteRepository,
    ILogger<CreateEstateCommandHandler> logger
) : IRequestHandler<CreateEstateCommand, CreateEstateCommandDto>
{
    public async Task<CreateEstateCommandDto> Handle(CreateEstateCommand command, CancellationToken cancellationToken)
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
        Role? ownerRole = null;

        foreach (var role in sharedRoles)
        {
            if (role.Name == "Owner")
            {
                ownerRole = role;
            }
            estate.EstateRoles.Add(new EstateRole()
            {
                RoleId = role.Id,
                EstateId = estate.Id
            });
        }

        if (ownerRole is null)
        {
            logger.LogError("Owner role not found in the database. Please check the database for the roles.");
            throw new Exception("An Error occurred while creating the estate. Please try again.");
        }

        logger.LogInformation("Adding user as the owner of the estate");

        var user = await userReadRepository.GetByIdAsync(command.UserId, cancellationToken)
            ?? throw new Exception($"User not found with id {command.UserId}");

        var estateUser = new EstateUser()
        {
            UserId = user.Id,
            EstateId = estate.Id,
            RoleId = ownerRole.Id,
            CreatedBy = command.UserId,
            EstateUserType = EstateUserType.Admin
        };

        await estateUserWriteRepository.AddAsync(estateUser, cancellationToken);

        await estateWriteRepository.AddAsync(estate, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Estate created with name {Name}", command.Name);

        return estate.Adapt<CreateEstateCommandDto>();
    }
}