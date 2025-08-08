using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Verbum.API.DTOs.Post;
using Verbum.API.Interfaces.Services;
using Verbum.API.Results;

namespace Verbum.API.Controllers;

[Controller]
[Route("api/[controller]")]
public class PostController : ControllerBase {
    private readonly IPostService _postService;

    public PostController(IPostService postService) {
        _postService = postService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPostById([FromRoute] int id) {
        string? userClaimsId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        ServiceResult<PostCompleteDto> result = await _postService.GetPostById(id, int.Parse(userClaimsId ?? "0"));
        return result.Status switch {
            ServiceResultStatus.Success => Ok(result.Data),
            ServiceResultStatus.NotFound => NotFound(new { message = "Post not found" }),
            _ => BadRequest(new { message = "Something went wrong" })
        };
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostDto dto) {
        string? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null) {
            return Unauthorized("User not logged in");
        }

        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }

        int userId = int.Parse(userIdClaim);
        ServiceResult<PostSimpleDto> result = await _postService.CreatePost(dto, userId);
        return result.Status switch {
            ServiceResultStatus.Success => Ok(result.Data),
            ServiceResultStatus.Unauthorized => Unauthorized(result.Message),
            ServiceResultStatus.NotFound => NotFound(result.Message),
            _ => BadRequest(new { message = "Something went wrong" })
        };
    }

    [Authorize]
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdatePost([FromRoute] int id, [FromBody] UpdatePostDto dto) {
        string? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null) {
            return Unauthorized("User not logged in!");
        }

        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }

        ServiceResult<PostCompleteDto> result = await _postService.UpdatePost(int.Parse(userIdClaim ?? "0"), id, dto);
        return result.Status switch {
            ServiceResultStatus.Success => Ok(result.Data),
            ServiceResultStatus.Unauthorized => Unauthorized(result.Message),
            ServiceResultStatus.NotFound => NotFound(result.Message),
            _ => BadRequest(new { message = "Something went wrong" })
        };
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost([FromRoute] int id) {
        string? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null) {
            return Unauthorized("User not logged in!");
        }

        ServiceResult<bool> result = await _postService.DeletePost(int.Parse(userIdClaim ?? "0"), id);
        return result.Status switch {
            ServiceResultStatus.Success => Ok("Post deleted"),
            ServiceResultStatus.Unauthorized => Unauthorized(result.Message),
            ServiceResultStatus.NotFound => NotFound(result.Message),
            _ => BadRequest(new { message = "Something went wrong" })
        };
    }

    [Authorize]
    [HttpPost("{id}/like")]
    public async Task<IActionResult> LikePost([FromRoute] int id) {
        string? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null) {
            return Unauthorized("User not logged in!");
        }

        ServiceResult<PostCompleteDto> result = await _postService.PostVote(int.Parse(userIdClaim), id, 1);
        return result.Status switch {
            ServiceResultStatus.Success => Ok(result.Data),
            ServiceResultStatus.Unauthorized => Unauthorized(result.Message),
            ServiceResultStatus.NotFound => NotFound(result.Message),
            _ => BadRequest(new { message = "Something went wrong" })
        };
    }

    [Authorize]
    [HttpPost("{id}/dislike")]
    public async Task<IActionResult> DislikePost([FromRoute] int id) {
        string? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null) {
            return Unauthorized("User not logged in!");
        }

        ServiceResult<PostCompleteDto> result = await _postService.PostVote(int.Parse(userIdClaim), id, -1);
        return result.Status switch {
            ServiceResultStatus.Success => Ok(result.Data),
            ServiceResultStatus.Unauthorized => Unauthorized(result.Message),
            ServiceResultStatus.NotFound => NotFound(result.Message),
            _ => BadRequest(new { message = "Something went wrong" })
        };
    }
}