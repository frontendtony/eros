using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using EstateManager.Models;
using EstateManager.Entities;
using EstateManager.Services;

namespace EstateManager.Controllers;

[ApiController]
[Route("[controller]")]
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

    [HttpPost(Name = "CreateUser")]
    public async Task<IActionResult> Create(CreateUserRequestModel user)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userManager.CreateAsync(
                new ApplicationUser()
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.Email
                },
                user.Password
            );

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // fetch the newly created user
            var CreatedUser = await _userManager.FindByEmailAsync(user.Email);

            if (CreatedUser == null)
            {
                return StatusCode(500);
            }

            var Response = new UserResponseModel()
            {
                Id = CreatedUser.Id,
                Email = CreatedUser.Email!,
                FirstName = CreatedUser.FirstName,
                LastName = CreatedUser.LastName,
                PhoneNumber = CreatedUser.PhoneNumber,
                Avatar = CreatedUser.Avatar
            };

            return CreatedAtRoute("CreateUser", new { id = Response.Id }, CreatedUser);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating user");
            return StatusCode(500);
        }
    }

    [HttpGet("{emailOrId}", Name = "GetUser")]
    [ProducesResponseType(typeof(UserResponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(UserResponseModel), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(string emailOrId)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(emailOrId);
            if (user == null)
            {
                user = await _userManager.FindByIdAsync(emailOrId);
            }

            if (user == null)
            {
                return NotFound();
            }

            return Ok(new UserResponseModel()
            {
                Id = user.Id,
                Email = user.Email!,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Avatar = user.Avatar
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching user");
            return StatusCode(StatusCodes.Status500InternalServerError, "Error fetching user");
        }
    }
}