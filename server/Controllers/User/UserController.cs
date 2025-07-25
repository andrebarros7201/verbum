using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Verbum.API.Interfaces.Services;

namespace Verbum.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase {
    private readonly IUserService _userService;

    public UserController(IUserService userService) {
        _userService = userService;
    }


    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> DeleteUser() {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        bool result = await _userService.DeleteUser(userId);
        return result ? Ok() : NotFound(new { message = "User not found" });
    }
}