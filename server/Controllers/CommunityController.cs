using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Verbum.API.DTOs.Community;
using Verbum.API.Interfaces.Services;
using Verbum.API.Results;

namespace Verbum.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommunityController : ControllerBase {
    private readonly ICommunityService _communityService;

    public CommunityController(ICommunityService communityService) {
        _communityService = communityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCommunities() {
        string? userClaimsId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        ServiceResult<List<CommunitySimpleDto>> result = await _communityService.GetCommunities(int.Parse(userClaimsId));
        return Ok(result.Data);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCommunityById([FromRoute] int id) {
        string? userClaimsId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        ServiceResult<CommunityCompleteDto>? result = await _communityService.GetCommunityById(id, int.Parse(userClaimsId ?? "0"));

        return result.Status switch {
            ServiceResultStatus.Success => Ok(result.Data),
            ServiceResultStatus.NotFound => NotFound(new { message = "Community not found" }),
            _ => BadRequest(new { message = "Something went wrong" })
        };
    }


    [HttpGet("search")]
    public async Task<IActionResult> GetCommunitiesByName([FromQuery] string name) {
        string? userClaimsId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        ServiceResult<List<CommunitySimpleDto>> result = await _communityService.GetCommunitiesByName(name, int.Parse(userClaimsId ?? "0"));
        return Ok(result.Data);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateCommunity([FromBody] CreateCommunityDto dto) {
        string? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null) {
            return Unauthorized("User not logged in");
        }

        int userId = int.Parse(userIdClaim);

        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }

        ServiceResult<CommunitySimpleDto>? result = await _communityService.CreateCommunity(dto, userId);

        return result.Status switch {
            ServiceResultStatus.Success => Ok(result.Data),
            ServiceResultStatus.Conflict => Conflict(result.Message),
            _ => BadRequest(new { message = "Something went wrong" })
        };
    }


    [Authorize]
    [HttpPatch("{id:int}")]
    public async Task<IActionResult> UpdateCommunity([FromRoute] int id, [FromBody] UpdateCommunityDto dto) {
        string? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null) {
            return Unauthorized();
        }

        int userId = int.Parse(userIdClaim);
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }

        ServiceResult<CommunitySimpleDto> result = await _communityService.UpdateCommunity(userId, id, dto);
        return result.Status switch {
            ServiceResultStatus.Success => Ok(result.Data),
            ServiceResultStatus.Unauthorized => Unauthorized(result.Message),
            ServiceResultStatus.NotFound => NotFound(result.Message),
            _ => BadRequest(new { message = "Something went wrong" })
        };
    }

    // Only the owner can delete the community
    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCommunity([FromRoute] int id) {
        string? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null) {
            return Unauthorized();
        }

        int userId = int.Parse(userIdClaim);
        ServiceResult<bool> result = await _communityService.DeleteCommunity(id, userId);
        return result.Status switch {
            ServiceResultStatus.Success => Ok("Community deleted"),
            ServiceResultStatus.Unauthorized => Unauthorized(result.Message),
            ServiceResultStatus.NotFound => NotFound(result.Message),
            _ => BadRequest(new { message = "Something went wrong" })
        };
    }

    [Authorize]
    [HttpPost("{id}/join")]
    public async Task<IActionResult> JoinCommunity([FromRoute] int id) {
        string? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null) {
            return Unauthorized("User not logged in");
        }

        int userId = int.Parse(userIdClaim);
        ServiceResult<bool> result = await _communityService.JoinCommunity(id, userId);
        return result.Status switch {
            ServiceResultStatus.Success => Ok("User joined community"),
            ServiceResultStatus.Error => BadRequest(result.Message),
            ServiceResultStatus.NotFound => NotFound(result.Message),
            _ => BadRequest(new { message = "Something went wrong" })
        };
    }

    [Authorize]
    [HttpPost("{id}/leave")]
    public async Task<IActionResult> LeaveCommunity([FromRoute] int id) {
        string? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null) {
            return Unauthorized();
        }

        int userId = int.Parse(userIdClaim);
        ServiceResult<bool> result = await _communityService.LeaveCommunity(id, userId);
        return result.Status switch {
            ServiceResultStatus.Success => Ok("User left community"),
            ServiceResultStatus.Error => BadRequest(result.Message),
            ServiceResultStatus.NotFound => NotFound(result.Message),
            _ => BadRequest(new { message = "Something went wrong" })
        };
    }
}