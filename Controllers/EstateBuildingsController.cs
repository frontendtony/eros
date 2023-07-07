using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EstateManager.Models;
using EstateManager.Repositories;
using System.Security.Claims;
using EstateManager.Entities;

namespace EstateManager.Controllers;

[ApiController]
[Authorize]
[Route("Estates/{estateId}/Buildings")]
public class EstateBuildingsController : ControllerBase
{
    private readonly ILogger<EstateBuildingsController> _logger;
    private readonly EstateManagerDbContext _dbContext;

    public EstateBuildingsController(ILogger<EstateBuildingsController> logger, EstateManagerDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    // create an estate building
    [HttpPost(Name = "CreateEstateBuilding")]
    public async Task<IActionResult> CreateEstateBuilding(Guid estateId, CreateEstateBuildingModel estateBuilding)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // verify estate ownership with logged in user
            var CurrentUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (CurrentUserId == null)
            {
                return Unauthorized();
            }

            // retrieve the estate from the database
            var Estate = await _dbContext.Estates.FindAsync(estateId);
            if (Estate == null)
            {
                return BadRequest("Invalid estate id");
            }

            try
            {
                var ExixtingEstateBuilding = await Task.Run(() => _dbContext.EstateBuildings.Where(B => B.Name == estateBuilding.Name).FirstOrDefault());

                if (ExixtingEstateBuilding != null)
                {
                    return StatusCode(StatusCodes.Status409Conflict, $"A building with name '{estateBuilding.Name}' already exists");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching existing estate building");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error verifying existing estate building");
            }

            // save the building to the estates_buildings table
            var NewEstateBuilding = new EstateBuilding()
            {
                Id = Guid.NewGuid(),
                EstateId = Estate.Id,
                CreatedBy = Guid.Parse(CurrentUserId),
                Name = estateBuilding.Name,
                Description = estateBuilding.Description ?? String.Empty,
            };

            try
            {
                _dbContext.EstateBuildings.Add(NewEstateBuilding);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to save esate building to the database");
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create estate building");
            }

            var routeValues = new { id = NewEstateBuilding.Id };

            return CreatedAtRoute("CreateEstateBuilding", routeValues, NewEstateBuilding);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create estate building");
            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create estate building");
        }
    }
}