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

        var CreatedUser = new UserResponseModel(
            user.Email,
            user.FirstName,
            user.LastName,
            user.PhoneNumber,
            user.Avatar
        );

        return Created("", CreatedUser);
    }

    [HttpGet("{emailOrId}", Name = "GetUser")]
    [ProducesResponseType(typeof(UserResponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(UserResponseModel), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(string emailOrId)
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

        return Ok(new UserResponseModel(
            user.Email!,
            user.FirstName,
            user.LastName,
            user.PhoneNumber,
            user.Avatar
        ));
    }
}