using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Verbum.API.DTOs.User;
using Verbum.API.Interfaces.Services;
using Verbum.API.Services;

namespace Verbum.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase {
    private readonly IAuthService _authService;
    private readonly TokenService _tokenService;

    public AuthController(IAuthService authService, TokenService tokenService) {
        _authService = authService;
        _tokenService = tokenService;
    }

    /// <summary>
    ///     Authenticates a user with their username and password.
    /// </summary>
    /// <param name="dto">Login credentials (username and password).</param>
    /// <response code="200">Returns the authenticated user</response>
    /// <response code="400">If the request model is invalid</response>
    /// <response code="404">If the user is not found or credentials are incorrect</response>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto dto) {
        if (!ModelState.IsValid) {
            return BadRequest();
        }

        var result = await _authService.Login(dto);

        if (result == null) {
            return NotFound(new { message = "Invalid credentials" });
        }

        string token = _tokenService.GenerateToken(result);

        Response.Cookies.Append("token", token,
            new CookieOptions { HttpOnly = true, Secure = false, Expires = DateTime.UtcNow.AddDays(7), SameSite = SameSiteMode.Lax });

        return Ok();
    }

    /// <summary>
    ///     Creates a user
    /// </summary>
    /// <param name="dto">Register credentials (username and password).</param>
    /// <response code="200">Returns the authenticated user</response>
    /// <response code="400">If the request model is invalid</response>
    /// <response code="409">If user already exitst</response>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserDto dto) {
        if (!ModelState.IsValid) {
            return BadRequest();
        }

        bool result = await _authService.Register(dto);

        return result ? Created() : Conflict("User already exists");
    }

    /// <summary>
    ///     Deletes the user JWT Token Cookie
    /// </summary>
    /// <response code="200">Deletes the token</response>
    [Authorize]
    [HttpGet("logout")]
    public async Task<IActionResult> Logout() {
        Response.Cookies.Delete("token");
        return Ok();
    }
}