using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Verbum.API.DTOs.Community;
using Verbum.API.Interfaces.Services;

namespace Verbum.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommunityController : ControllerBase {
    private readonly ICommunityService _communityService;

    public CommunityController(ICommunityService communityService) {
        _communityService = communityService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCommunityById([FromRoute] int id) {
        var result = await _communityService.GetCommunityById(id);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetCommunities() {
        List<CommunityDto> result = await _communityService.GetCommunities();
        return Ok(result);
    }

    [HttpGet("search")]
    public async Task<IActionResult> GetCommunitiesByName([FromQuery] string name) {
        List<CommunityDto> result = await _communityService.GetCommunitiesByName(name);
        return Ok(result);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateCommunity([FromBody] CreateCommunityDto dto) {
        string? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null) {
            return Unauthorized();
        }

        int userId = int.Parse(userIdClaim);

        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }

        var result = await _communityService.CreateCommunity(dto, userId);

        if (result == null) {
            return Conflict(new { message = "Community already exists" });
        }

        return Ok(result);
    }

    [HttpPost("{id}/join")]
    public async Task<IActionResult> JoinCommunity([FromRoute] int id) {
        string? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null) {
            return Unauthorized();
        }

        int userId = int.Parse(userIdClaim);
        bool result = await _communityService.JoinCommunity(id, userId);
        return result ? Ok() : BadRequest(new { message = "Something went wrong" });
    }

    [HttpPost("{id}/leave")]
    public async Task<IActionResult> LeaveCommunity([FromRoute] int id) {
        string? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null) {
            return Unauthorized();
        }

        int userId = int.Parse(userIdClaim);
        bool result = await _communityService.LeaveCommunity(id, userId);
        return result ? Ok() : BadRequest(new { message = "Something went wrong" });
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCommunity([FromRoute] int id) {
        string? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null) {
            return Unauthorized();
        }

        int userId = int.Parse(userIdClaim);
        bool result = await _communityService.DeleteCommunity(id, userId);
        return result ? Ok() : BadRequest(new { message = "Something went wrong" });
    }

    // TODO implement the update community
}