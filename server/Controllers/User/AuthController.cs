using Microsoft.AspNetCore.Mvc;
using Verbum.API.DTOs.User;
using Verbum.API.Interfaces.Services;

namespace Verbum.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase {
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService) {
        _authService = authService;
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

        return Ok(result);
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
}