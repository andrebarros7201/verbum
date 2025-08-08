using Verbum.API.DTOs.Post;
using Verbum.API.Results;

namespace Verbum.API.Interfaces.Services;

public interface IPostService {
    Task<ServiceResult<PostCompleteDto>> GetPostById(int id, int userId);
    Task<ServiceResult<PostSimpleDto>> CreatePost(CreatePostDto dto, int userId);
    Task<ServiceResult<PostCompleteDto>> UpdatePost(int userId, int postId, UpdatePostDto dto);
    Task<ServiceResult<bool>> DeletePost(int userId, int postId);
    Task<ServiceResult<PostCompleteDto>> PostVote(int userId, int postId, int value);
}