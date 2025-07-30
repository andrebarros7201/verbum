using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Verbum.API.DTOs.User;
using Verbum.API.Interfaces.Services;
using Verbum.API.Results;
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
    [HttpGet("me")]
    public async Task<IActionResult> GetMe() {
        string? userClaimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userClaimId == null) {
            return Unauthorized();
        }

        ServiceResult<UserCompleteDto> result = await _userService.GetUserCompleteById(int.Parse(userClaimId));
        return result.Status switch {
            ServiceResultStatus.Success => Ok(result.Data),
            ServiceResultStatus.NotFound => NotFound(new { message = "User not found" }),
            _ => BadRequest(new { message = "Something went wrong" })
        };
    }

    [Authorize]
    [HttpPatch]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto dto) {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }

        string? userClaimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userClaimId == null) {
            return Unauthorized("User not logged in!");
        }

        dto.Id = int.Parse(userClaimId);

        ServiceResult<UserSimpleDto> result = await _userService.UpdateUser(dto);

        if (result.Status == ServiceResultStatus.NotFound || result.Data == null) {
            return NotFound(result.Message);
        }

        if (result.Status == ServiceResultStatus.Conflict) {
            return Conflict(new { message = result.Message });
        }

        string token = _tokenService.GenerateToken(result.Data);

        Response.Cookies.Append("token", token,
            new CookieOptions { HttpOnly = true, Secure = true, Expires = DateTime.UtcNow.AddDays(7), SameSite = SameSiteMode.None });

        return Ok("User updated!");
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> DeleteUser() {
        string? userClaimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userClaimId == null) {
            return Unauthorized("User not logged in!");
        }

        int userId = int.Parse(userClaimId);

        ServiceResult<bool> result = await _userService.DeleteUser(userId);

        return result.Status switch {
            ServiceResultStatus.Success => Ok("User deleted"),
            ServiceResultStatus.Unauthorized => Unauthorized(result.Message),
            ServiceResultStatus.NotFound => NotFound(result.Message),
            _ => BadRequest(new { message = "Something went wrong" })
        };
    }
}