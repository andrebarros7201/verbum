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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id) {
        bool result = await _userService.DeleteUser(id);
        return result ? Ok() : NotFound(new { message = "User not found" });
    }
}