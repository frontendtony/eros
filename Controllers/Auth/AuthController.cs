using EstateManager.Models;
using EstateManager.Services;
using EstateManager.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EstateManager.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JwtService _jwtService;

    public AuthController(ILogger<AuthController> logger, UserManager<ApplicationUser> userManager, JwtService jwtService)
    {
        _logger = logger;
        _userManager = userManager;
        _jwtService = jwtService;
    }

    [HttpPost("token", Name = "CreateBearerToken")]
    [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBearerToken([FromBody] TokenRequest request)
    {
        if (!ModelState.IsValid || request.Email == null || request.Password == null)
        {
            return BadRequest("Invalid credentials");
        }

        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            return BadRequest("Invalid email address");
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!isPasswordValid)
        {
            return BadRequest("Password is invalid");
        }

        var token = _jwtService.CreateToken(user);

        return Ok(token);
    }

    [HttpPost("signup", Name = "Signup")]
    [ProducesResponseType(typeof(UserResponseModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] SignupRequestModel user)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var UserId = Guid.NewGuid().ToString();
            var CurrentTime = DateTime.UtcNow;

            var NewUser = new ApplicationUser()
            {
                Id = UserId,
                Email = user.Email,
                UserName = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Avatar = user.Avatar,
                CreatedAt = CurrentTime,
            };

            var result = await _userManager.CreateAsync(NewUser, user.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            var Token = _jwtService.CreateToken(NewUser);

            var NewUserResponse = new UserResponseModel()
            {
                Id = UserId,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Avatar = user.Avatar,
                Token = Token.Token,
                CreatedAt = NewUser.CreatedAt,
                UpdatedAt = NewUser.CreatedAt,
            };

            return CreatedAtRoute("Signup", new { id = NewUser.Id }, NewUserResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating user");
            return StatusCode(StatusCodes.Status500InternalServerError, "Error creating user account");
        }
    }
}