using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using EstateManager.Models;
using EstateManager.Entities;
using Microsoft.AspNetCore.Authorization;

namespace EstateManager.Controllers;

[ApiController]
[Route("api/users")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly UserManager<ApplicationUser> _userManager;

    public UsersController(ILogger<UsersController> logger, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }

    [HttpGet("{id:guid}", Name = "GetUser")]
    [ProducesResponseType(typeof(UserResponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(string id)
    {
        try
        {
            var User = await _userManager.FindByIdAsync(id) ?? await _userManager.FindByEmailAsync(id);
            if (User is null)
            {
                return NotFound();
            }

            return Ok(new UserResponseModel()
            {
                Id = User.Id,
                Email = User.Email ?? "",
                FirstName = User.FirstName,
                LastName = User.LastName,
                PhoneNumber = User.PhoneNumber,
                Avatar = User.Avatar,
                CreatedAt = User.CreatedAt,
                UpdatedAt = User.UpdatedAt,
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching user");
            return StatusCode(StatusCodes.Status500InternalServerError, "Error fetching user");
        }
    }
}