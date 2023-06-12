using EstateManager.Entities;
using EstateManager.Models;
using EstateManager.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EstateManager.Controllers;

[ApiController]
[Route("[controller]")]
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
    public IEnumerable<EstateModel> Get()
    {
        return _dbContext.Estates.Select(e => new EstateModel(
            e.Id,
            e.Name,
            e.Country,
            e.StateProvince,
            e.City,
            e.Address,
            e.ZipCode,
            e.LatLng
        ));
    }

    [HttpGet("{id}", Name = "GetEstate")]
    [ProducesResponseType(typeof(EstateModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(EstateModel), StatusCodes.Status404NotFound)]
    public IActionResult Get(Guid id)
    {
        var estate = _dbContext.Estates.Find(id);
        if (estate == null)
        {
            return NotFound();
        }

        return Ok(new EstateModel(
            estate.Id,
            estate.Name,
            estate.Country,
            estate.StateProvince,
            estate.City,
            estate.Address,
            estate.ZipCode,
            estate.LatLng
        ));
    }

    [HttpPost(Name = "CreateEstate")]
    public ActionResult<EstateModel> Post(EstateModel model)
    {
        var estate = new Estate
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            Country = model.Country,
            StateProvince = model.StateProvince,
            City = model.City,
            Address = model.Address,
            ZipCode = model.ZipCode,
            LatLng = model.LatLng ?? string.Empty
        };

        try
        {
            _dbContext.Estates.Add(estate);
            _dbContext.SaveChanges();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create estate.");
        }

        return CreatedAtRoute("GetEstate", new { id = estate.Id }, new EstateModel(
            estate.Id,
            estate.Name,
            estate.Country,
            estate.StateProvince,
            estate.City,
            estate.Address,
            estate.ZipCode,
            estate.LatLng
        ));
    }

    [HttpPut("{id}", Name = "UpdateEstate")]
    public ActionResult<EstateModel> Put(Guid id, EstateModel model)
    {
        var estate = _dbContext.Estates.Find(id);
        if (estate == null)
        {
            return NotFound();
        }

        estate.Name = model.Name;
        estate.Country = model.Country;
        estate.StateProvince = model.StateProvince;
        estate.City = model.City;
        estate.Address = model.Address;
        estate.ZipCode = model.ZipCode;
        estate.LatLng = model.LatLng;

        _dbContext.SaveChanges();

        return new EstateModel(
            estate.Id,
            estate.Name,
            estate.Country,
            estate.StateProvince,
            estate.City,
            estate.Address,
            estate.ZipCode,
            estate.LatLng
        );
    }

    [HttpDelete("{id}", Name = "DeleteEstate")]
    public IActionResult Delete(Guid id)
    {
        var estate = _dbContext.Estates.Find(id);
        if (estate == null)
        {
            return NotFound();
        }

        try
        {
            _dbContext.Estates.Remove(estate);
            _dbContext.SaveChanges();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return NoContent();
    }
}