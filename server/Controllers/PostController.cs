using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Verbum.API.DTOs.Post;
using Verbum.API.Interfaces.Services;

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
        var result = await _postService.GetPostById(id, int.Parse(userClaimsId ?? "0"));
        return result != null ? Ok(result) : NotFound(new { message = "Post not found" });
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostDto dto) {
        string? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null) {
            return Unauthorized();
        }

        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }

        int userId = int.Parse(userIdClaim);
        var result = await _postService.CreatePost(dto, userId);
        return result != null ? Ok(result) : BadRequest(new { message = "Something went wrong" });
    }

    [Authorize]
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdatePost([FromRoute] int id, [FromBody] UpdatePostDto dto) {
        string? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null) {
            return Unauthorized();
        }

        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }

        var result = await _postService.UpdatePost(int.Parse(userIdClaim ?? "0"), id, dto);
        return result != null ? Ok(result) : BadRequest(new { message = "Something went wrong" });
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost([FromRoute] int id) {
        string? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null) {
            return Unauthorized();
        }

        bool result = await _postService.DeletePost(int.Parse(userIdClaim ?? "0"), id);
        return result ? Ok() : BadRequest(new { message = "Something went wrong" });
    }

    [Authorize]
    [HttpPost("{id}/like")]
    public async Task<IActionResult> LikePost([FromRoute] int id) {
        string? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null) {
            return Unauthorized();
        }

        var result = await _postService.PostVote(int.Parse(userIdClaim), id, 1);
        var updatedPost = await _postService.GetPostById(id, int.Parse(userIdClaim));
        ;
        return result != null ? Ok(updatedPost) : BadRequest(new { message = "Something went wrong" });
    }
}