using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EstateManager.Models;
using EstateManager.Entities;
using EstateManager.Repositories;
using Api.ResponseModels;
using EstateManager.DbContexts;

namespace EstateManager.Controllers;

[ApiController]
[Authorize(Policy = "AdminOnly")]
[Route("api/roles")]
public class RolesController : ControllerBase
{
    private readonly ILogger<RolesController> _logger;
    private readonly EstateManagerDbContext _dbContext;

    public RolesController(ILogger<RolesController> logger, EstateManagerDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet(Name = "GetRoles")]
    [ProducesResponseType(typeof(List<RoleResponseModel>), StatusCodes.Status200OK)]
    public IActionResult GetRoles()
    {
        // fetch all roles and include the associated permissions from the role_permissions table
        var Roles = _dbContext.EstateRoles.Select(r => new RoleResponseModel()
        {
            Id = r.Id,
            Name = r.Name,
            Permissions = r.Permissions
                .Select(rp => rp.EstatePermission!)
                .ToList()
        }).ToList();

        return Ok(Roles);
    }

    [HttpGet("{id:guid}", Name = "GetRole")]
    [ProducesResponseType(typeof(RoleResponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid id)
    {
        // select the role and include the associated permissions from the role_permissions table
        var RoleWithPermissions = await Task.Run(() => _dbContext.EstateRoles
            .Where(r => r.Id == id)
            .Select(r => new RoleResponseModel()
            {
                Id = r.Id,
                Name = r.Name,
                Permissions = r.Permissions
                    .Select(rp => rp.EstatePermission!)
                    .ToList()
            }).FirstOrDefault());

        if (RoleWithPermissions == null)
        {
            return NotFound();
        }

        return Ok(RoleWithPermissions);
    }

    [HttpPost(Name = "CreateRole")]
    [ProducesResponseType(typeof(RoleResponseModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleModel request)
    {
        try
        {
            // check if the role with same name already exists
            bool existingRole = await Task.Run(() => _dbContext.EstateRoles.Any(r => r.Name == request.Name));
            if (existingRole)
            {
                return StatusCode(StatusCodes.Status409Conflict, $"Role with name '{request.Name}' already exists");
            }

            var CurrentTime = DateTime.UtcNow;

            var NewRole = new EstateRole()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                CreatedAt = CurrentTime,
                UpdatedAt = CurrentTime,
            };

            // start a transaction
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            {
                // create the role
                await _dbContext.EstateRoles.AddAsync(NewRole);
                await _dbContext.SaveChangesAsync();

                // add the permissions
                foreach (var permission in request.Permissions)
                {
                    var rolePermission = new EstateRolePermission()
                    {
                        Id = Guid.NewGuid(),
                        EstateRoleId = NewRole.Id,
                        EstatePermissionId = permission,
                    };

                    await _dbContext.EstateRolePermissions.AddAsync(rolePermission);
                }

                _dbContext.SaveChanges();
                transaction.Commit();
            }

            return CreatedAtRoute("CreateRole", new { id = NewRole.Id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating role");
            return StatusCode(StatusCodes.Status500InternalServerError, "Error creating role");
        }
    }

    [HttpPut("{id:guid}", Name = "UpdateRole")]
    [ProducesResponseType(typeof(RoleResponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateRole(Guid id, [FromBody] UpdateRoleModel request)
    {
        try
        {
            var Role = await _dbContext.EstateRoles.FindAsync(id);

            if (Role == null)
            {
                // create the role if it doesn't exist
                var NewRole = new CreateRoleModel()
                {
                    Name = request.Name,
                    Description = request.Description,
                    Permissions = request.Permissions,
                };
                return await CreateRole(NewRole);
            }

            Role.Name = request.Name;
            Role.Description = request.Description;

            // start a transaction
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            {
                // update the role
                _dbContext.EstateRoles.Update(Role);
                await _dbContext.SaveChangesAsync();

                // delete the existing permissions
                var existingPermissions = await Task.Run(() => _dbContext.EstateRolePermissions.Where(p => p.EstateRoleId == id));
                _dbContext.EstateRolePermissions.RemoveRange(existingPermissions);
                await _dbContext.SaveChangesAsync();

                // add the permissions
                foreach (var permission in request.Permissions)
                {
                    var rolePermission = new EstateRolePermission()
                    {
                        Id = Guid.NewGuid(),
                        EstateRoleId = Role.Id,
                        EstatePermissionId = permission,
                    };

                    await _dbContext.EstateRolePermissions.AddAsync(rolePermission);
                }

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }

            var ReturnRole = _dbContext.EstateRoles.Where(r => r.Id == id).Select(r => new RoleResponseModel()
            {
                Id = r.Id,
                Name = r.Name,
                Permissions = r.Permissions
                    .Select(rp => rp.EstatePermission!)
                    .ToList()
            });

            return Ok(ReturnRole);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating role");
            return StatusCode(StatusCodes.Status500InternalServerError, "Error updating role");
        }
    }

    [HttpDelete("{id:guid}", Name = "DeleteRole")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRole(Guid id)
    {
        try
        {
            var Role = await _dbContext.EstateRoles.FindAsync(id);
            if (Role == null)
            {
                return NotFound();
            }

            _dbContext.EstateRoles.Remove(Role);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting role");
            return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting role");
        }
    }
}

