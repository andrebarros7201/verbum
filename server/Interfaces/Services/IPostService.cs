using Verbum.API.DTOs.Post;

namespace Verbum.API.Interfaces.Services;

public interface IPostService {
    Task<PostCompleteDto> GetPostById(int id);
    Task<List<PostSimpleDto>> GetPostsByCommunityId(int communityId);
    Task<PostSimpleDto> CreatePost(CreatePostDto dto, int userId);
    Task<PostSimpleDto> UpdatePost(int userId, int postId, UpdatePostDto dto);
    Task<bool> DeletePost(int userId, int postId);
}