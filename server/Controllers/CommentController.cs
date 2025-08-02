using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Verbum.API.DTOs.Comment;
using Verbum.API.Interfaces.Services;
using Verbum.API.Results;

namespace Verbum.API.Controllers;

[ApiController]
[Route("api")]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [Authorize]
    [HttpPost("{postId:int}/comment")]
    public async Task<IActionResult> AddComment([FromRoute] int postId, [FromBody] CreateCommentDto dto)
    {
        string? userClaimsId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userClaimsId == null)
        {
            return Unauthorized();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        ServiceResult<CommentDto> result = await _commentService.AddComment(int.Parse(userClaimsId), postId, dto);

        return result.Status switch
        {
            ServiceResultStatus.Success => Ok(result.Data),
            ServiceResultStatus.Unauthorized => Unauthorized(result.Message),
            ServiceResultStatus.NotFound => NotFound(result.Message),
            _ => BadRequest(new { message = "Something went wrong" })
        };
    }

    [Authorize]
    [HttpDelete("comment/{commentId:int}")]
    public async Task<IActionResult> DeleteComment([FromRoute] int commentId)
    {
        string? userClaimsId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userClaimsId == null)
        {
            return Unauthorized();
        }

        ServiceResult<bool> result = await _commentService.DeleteComment(int.Parse(userClaimsId), commentId);

        return result.Status switch
        {
            ServiceResultStatus.Success => Ok("Comment deleted"),
            ServiceResultStatus.Unauthorized => Unauthorized(result.Message),
            ServiceResultStatus.NotFound => NotFound(result.Message),
            _ => BadRequest(new { message = "Something went wrong" })
        };
    }

    [Authorize]
    [HttpPatch("comment/{commentId:int}")]
    public async Task<IActionResult> UpdateComment([FromRoute] int commentId, [FromBody] UpdateCommentDto dto)
    {
        string? userClaimsId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userClaimsId == null)
        {
            return Unauthorized();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        ServiceResult<CommentDto> result = await _commentService.UpdateComment(int.Parse(userClaimsId), commentId, dto);
        return result.Status switch
        {
            ServiceResultStatus.Success => Ok(result.Data),
            ServiceResultStatus.Unauthorized => Unauthorized(result.Message),
            ServiceResultStatus.NotFound => NotFound(result.Message),
            _ => BadRequest(new { message = "Something went wrong" })
        };
    }

    [Authorize]
    [HttpPost("comment/{commentId:int}/like")]
    public async Task<IActionResult> LikeComment([FromRoute] int commentId)
    {
        string? userClaimsId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userClaimsId == null)
        {
            return Unauthorized("User is not logged in!");
        }

        ServiceResult<CommentDto> result = await _commentService.CommentVote(int.Parse(userClaimsId), commentId, 1);
        return result.Status switch
        {
            ServiceResultStatus.Success => Ok(result.Data),
            ServiceResultStatus.Unauthorized => Unauthorized(result.Message),
            ServiceResultStatus.NotFound => NotFound(result.Message),
            _ => BadRequest(new { message = "Something went wrong" })
        };
    }

    [Authorize]
    [HttpPost("comment/{commentId:int}/dislike")]
    public async Task<IActionResult> DislikeComment([FromRoute] int commentId)
    {
        string? userClaimsId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userClaimsId == null)
        {
            return Unauthorized("User is not logged in!");
        }

        ServiceResult<CommentDto> result = await _commentService.CommentVote(int.Parse(userClaimsId), commentId, -1);
        return result.Status switch
        {
            ServiceResultStatus.Success => Ok(result.Data),
            ServiceResultStatus.Unauthorized => Unauthorized(result.Message),
            ServiceResultStatus.NotFound => NotFound(result.Message),
            _ => BadRequest(new { message = "Something went wrong" })
        };
    }
}
