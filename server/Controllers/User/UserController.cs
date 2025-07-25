using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Verbum.API.DTOs.User;
using Verbum.API.Interfaces.Services;
using Verbum.API.Services;

namespace Verbum.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase {
    private readonly TokenService _tokenService;
    private readonly IUserService _userService;

    public UserController(IUserService userService, TokenService tokenService) {
        _userService = userService;
        _tokenService = tokenService;
    }

    [Authorize]
    [HttpPatch]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto dto) {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }

        string? userClaimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userClaimId == null) {
            return Unauthorized();
        }

        dto.Id = int.Parse(userClaimId);

        var result = await _userService.UpdateUser(dto);
        if (result == null) {
            return NotFound(new { message = "User not found" });
        }

        string token = _tokenService.GenerateToken(result);

        Response.Cookies.Append("token", token,
            new CookieOptions { HttpOnly = true, Secure = true, Expires = DateTime.UtcNow.AddDays(7), SameSite = SameSiteMode.Strict });

        return Ok();
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> DeleteUser() {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        bool result = await _userService.DeleteUser(userId);
        return result ? Ok() : NotFound(new { message = "User not found" });
    }
}