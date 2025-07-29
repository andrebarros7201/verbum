using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Verbum.API.DTOs.User;
using Verbum.API.Interfaces.Services;
using Verbum.API.Results;
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
    /// <response code="401">Wrong credentials</response>
    /// <response code="404">If the user is not found or credentials are incorrect</response>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto dto) {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }

        ServiceResult<UserSimpleDto> result = await _authService.Login(dto);

        if (result.Status != ServiceResultStatus.Success) {
            return result.Status switch {
                ServiceResultStatus.NotFound => NotFound(result.Message),
                ServiceResultStatus.Unauthorized => Unauthorized(result.Message),
                _ => BadRequest("Something went wrong")
            };
        }

        var user = result.Data;
        string token = _tokenService.GenerateToken(user);
        Response.Cookies.Append("token", token,
            new CookieOptions {
                HttpOnly = true,
                Secure = false,
                Expires = DateTime.UtcNow.AddDays(7),
                SameSite = SameSiteMode.Lax
            });

        return Ok(user);
    }

    /// <summary>
    ///     Creates a user
    /// </summary>
    /// <param name="dto">Register credentials (username and password).</param>
    /// <response code="200">Returns the authenticated user</response>
    /// <response code="400">If the request model is invalid</response>
    /// <response code="409">If the user already exists</response>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserDto dto) {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }

        ServiceResult<bool> result = await _authService.Register(dto);

        return result.Status switch {
            ServiceResultStatus.Success => Ok(),
            ServiceResultStatus.Conflict => Conflict(result.Message),
            _ => BadRequest("Something went wrong")
        };
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