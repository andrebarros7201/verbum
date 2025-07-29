using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Verbum.API.DTOs.Comment;
using Verbum.API.Interfaces.Services;
using Verbum.API.Results;

namespace Verbum.API.Controllers;

[ApiController]
[Route("api")]
public class CommentController : ControllerBase {
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService) {
        _commentService = commentService;
    }

    [Authorize]
    [HttpPost("{postID:int}/comment")]
    public async Task<IActionResult> AddComment([FromRoute] int postID, [FromBody] CreateCommentDto dto) {
        string? userClaimsId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userClaimsId == null) {
            return Unauthorized();
        }

        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }

        ServiceResult<CommentDto> result = await _commentService.AddComment(int.Parse(userClaimsId), postID, dto);

        return result.Status switch {
            ServiceResultStatus.Success => Ok(result.Data),
            ServiceResultStatus.Unauthorized => Unauthorized(result.Message),
            ServiceResultStatus.NotFound => NotFound(result.Message),
            _ => BadRequest(new { message = "Something went wrong" })
        };
    }
}