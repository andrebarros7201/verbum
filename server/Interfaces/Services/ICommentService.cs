using Verbum.API.DTOs.Comment;
using Verbum.API.Results;

namespace Verbum.API.Interfaces.Services;

public interface ICommentService
{
    Task<ServiceResult<CommentDto>> AddComment(int userId, int postId, CreateCommentDto dto);
    Task<ServiceResult<CommentDto>> UpdateComment(int userId, int commentId, UpdateCommentDto dto);
    Task<ServiceResult<bool>> DeleteComment(int userId, int commentId);
    Task<ServiceResult<CommentDto>> CommentVote(int userId, int commentId, int value);
}
