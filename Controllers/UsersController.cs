using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using EstateManager.Models;
using EstateManager.Entities;
using EstateManager.Services;
using Microsoft.AspNetCore.Authorization;

namespace EstateManager.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JwtService _jwtService;

    public UsersController(ILogger<UsersController> logger, UserManager<ApplicationUser> userManager, JwtService jwtService)
    {
        _logger = logger;
        _userManager = userManager;
        _jwtService = jwtService;
    }

    [HttpGet("{id}", Name = "GetUser")]
    [ProducesResponseType(typeof(UserResponseModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(string id)
    {
        try
        {
            // find by id
            var User = await _userManager.FindByIdAsync(id);
            if (User == null)
            {
                // try finding by email
                User = await _userManager.FindByEmailAsync(id);
            }

            if (User == null)
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
                Avatar = User.Avatar
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching user");
            return StatusCode(StatusCodes.Status500InternalServerError, "Error fetching user");
        }
    }
}