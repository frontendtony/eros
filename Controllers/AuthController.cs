using EstateManager.Models;
using EstateManager.Services;
using EstateManager.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EstateManager.Controllers;

[ApiController]
[Route("[controller]")]
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
    public async Task<ActionResult<TokenResponse>> CreateBearerToken(TokenRequest request)
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
}