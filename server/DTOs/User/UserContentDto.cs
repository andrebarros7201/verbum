using Verbum.API.DTOs.Comment;
using Verbum.API.DTOs.Community;
using Verbum.API.DTOs.Post;

namespace Verbum.API.DTOs.User;

public class UserContentDto {
    public List<CommunitySimpleDto> Communities { get; set; } = new();
    public List<PostSimpleDto> Posts { get; set; } = new();
    public List<CommentDto> Comments { get; set; } = new();
}