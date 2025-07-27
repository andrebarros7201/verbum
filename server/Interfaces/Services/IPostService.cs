using Verbum.API.DTOs.Post;

namespace Verbum.API.Interfaces.Services;

public interface IPostService {
    Task<PostDto> GetPostById(int id);
    Task<List<PostDto>> GetPostsByCommunityId(int communityId);
    Task<PostDto> CreatePost(CreatePostDto dto, int userId);
    Task<PostDto> UpdatePost(int userId, int postId, UpdatePostDto dto);
    Task<bool> DeletePost(int userId, int postId);
}