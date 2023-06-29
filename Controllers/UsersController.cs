using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using EstateManager.Models;
using EstateManager.Services;

namespace EstateManager.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtService _jwtService;

    public UsersController(ILogger<UsersController> logger, UserManager<IdentityUser> userManager, JwtService jwtService)
    {
        _logger = logger;
        _userManager = userManager;
        _jwtService = jwtService;
    }

    [HttpPost(Name = "CreateUser")]
    public async Task<IActionResult> Create(UserModel user)
    {
        if (!ModelState.IsValid || user.Password == null)
        {
            return BadRequest(ModelState);
        }

        var result = await _userManager.CreateAsync(
            new IdentityUser() { Email = user.Email, UserName = user.Email },
            user.Password
        );

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        user.Password = null;
        return Created("", user);
    }

    [HttpGet("{emailOrId}", Name = "GetUser")]
    [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(UserModel), StatusCodes.Status404NotFound)]
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

        return Ok(new UserModel()
        {
            Email = user.Email,
            UserName = user.UserName
        });
    }
}