using Verbum.API.DTOs.Comment;
using Verbum.API.DTOs.Community;
using Verbum.API.DTOs.Post;

namespace Verbum.API.DTOs.User;

public class UserContentDto {
    public List<CommunityDto> Communities { get; set; } = new();
    public List<PostDto> Posts { get; set; } = new();
    public List<CommentDto> Comments { get; set; } = new();
}