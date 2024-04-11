using Eros.Api.Dto.Roles;
using Eros.Application.Exceptions;
using Eros.Application.Features.Roles.Commands;
using Eros.Domain.Aggregates.Roles;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Eros.Application.Features.Roles.CommandHandlers;

public class CreateSharedRoleCommandHandler(
        IRoleReadRepository roleReadRepository,
        IRoleWriteRepository roleWriteRepository,
        IPermissionReadRepository permissionReadRepository,
        ILogger<CreateSharedRoleCommandHandler> logger
) : IRequestHandler<CreateSharedRoleCommand, CreateSharedRoleCommandDto>
{
    public async Task<CreateSharedRoleCommandDto> Handle(CreateSharedRoleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating shared role. Name: {Name}", request.Name);
        
        var role = await roleReadRepository.GetByNameAsync(request.Name, cancellationToken);
        
        if (role is not null)
        {
            logger.LogWarning("Role with name {Name} already exists", request.Name);
            throw new BadRequestException("Role with this name already exists");
        }
        
        var newRole = Role.CreateSharedRole(request.Name, request.Description);
        
        logger.LogInformation("Role created. Name: {Name}", request.Name);
        
        logger.LogInformation("Adding permissions to role");
        foreach (var id in request.PermissionIds)
        {
            var permission = await permissionReadRepository.GetByIdAsync(id);
            
            if (permission is null)
            {
                logger.LogWarning("Permission with id {Id} not found", id);
                throw new NotFoundException($"Permission not found. Id: {id}");
            }
            
            newRole.Permissions.Add(permission);
        }
        
        logger.LogInformation("Saving role to database");
        await roleWriteRepository.AddAsync(newRole, cancellationToken);
        logger.LogInformation("Role saved to database. Name: {Name}", request.Name);

        return newRole.Adapt<CreateSharedRoleCommandDto>();
    }
}
