using EstateManager.Entities;
using EstateManager.Models;
using EstateManager.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace EstateManager.Controllers;

[ApiController]
[Route("api/estates")]
[Authorize]
public class EstatesController : ControllerBase
{
    private readonly ILogger<EstatesController> _logger;
    private readonly EstateManagerDbContext _dbContext;

    public EstatesController(ILogger<EstatesController> logger, EstateManagerDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet(Name = "GetEstates")]
    [ProducesResponseType(typeof(List<EstateResponseModel>), StatusCodes.Status200OK)]
    public IActionResult GetEstates()
    {
        var estates = _dbContext.Estates.ToList();
        return Ok(estates);
    }

    [HttpGet("{id:guid}", Name = "GetEstate")]
    [ProducesResponseType(typeof(EstateResponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid id)
    {
        var estate = await _dbContext.Estates.FindAsync(id);
        if (estate == null)
        {
            return NotFound();
        }

        return Ok(estate);
    }

    [HttpPost(Name = "CreateEstate")]
    [ProducesResponseType(typeof(EstateResponseModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult CreateEstate([FromBody] CreateEstateModel request)
    {
        try
        {
            // get the current user id;
            var currentUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (currentUserId == null)
            {
                return Unauthorized();
            }

            bool existingEstate = _dbContext.Estates.Any(e => e.Name == request.Name);
            if (existingEstate)
            {
                return StatusCode(StatusCodes.Status409Conflict, $"Estate with name {request.Name} already exists");
            }

            var CurrentTime = DateTime.UtcNow;

            var NewEstate = new Estate
            {
                Id = Guid.NewGuid(),
                CreatedBy = Guid.Parse(currentUserId),
                Name = request.Name,
                Country = request.Country,
                State = request.State,
                City = request.City,
                Address = request.Address,
                ZipCode = request.ZipCode,
                LatLng = request.LatLng ?? string.Empty,
                CreatedAt = CurrentTime,
                UpdatedAt = CurrentTime,
            };

            try
            {
                _dbContext.Estates.Add(NewEstate);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create estate.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create estate.");
            }

            return CreatedAtRoute("CreateEstate", new { id = NewEstate.Id }, NewEstate);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create estate.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create estate.");
        }
    }

    [HttpPut("{id}", Name = "UpdateEstate")]
    [ProducesResponseType(typeof(EstateResponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateEstate(Guid id, [FromBody] UpdateEstateRequest request)
    {
        try
        {
            // validate the request model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var estate = await _dbContext.Estates.FindAsync(id);
            if (estate == null)
            {
                return NotFound();
            }

            estate.Name = request.Name;
            estate.Country = request.Country;
            estate.State = request.State;
            estate.City = request.City;
            estate.Address = request.Address;
            estate.ZipCode = request.ZipCode;
            estate.LatLng = request.LatLng;
            estate.UpdatedAt = DateTime.UtcNow;

            try
            {
                _dbContext.Estates.Update(estate);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                _logger.LogError("Failed to save changes to estate.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(estate);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update estate.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update estate.");
        }
    }

    [HttpDelete("{id}", Name = "DeleteEstate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteEstate(Guid id)
    {
        try
        {
            var estate = _dbContext.Estates.Find(id);
            if (estate == null)
            {
                return NotFound();
            }


            _dbContext.Estates.Remove(estate);
            _dbContext.SaveChanges();


            return NoContent();
        }
        catch (Exception)
        {
            _logger.LogError("Failed to delete estate.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}