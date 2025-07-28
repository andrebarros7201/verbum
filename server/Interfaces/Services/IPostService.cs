using Verbum.API.DTOs.Post;

namespace Verbum.API.Interfaces.Services;

public interface IPostService {
    Task<PostCompleteDto> GetPostById(int id, int userId);
    Task<PostSimpleDto> CreatePost(CreatePostDto dto, int userId);
    Task<PostSimpleDto> UpdatePost(int userId, int postId, UpdatePostDto dto);
    Task<bool> DeletePost(int userId, int postId);
    Task<PostCompleteDto?> PostVote(int userId, int postId, int value);
}