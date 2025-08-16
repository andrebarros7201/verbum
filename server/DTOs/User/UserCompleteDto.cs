using Verbum.API.DTOs.Comment;
using Verbum.API.DTOs.Community;
using Verbum.API.DTOs.Post;

namespace Verbum.API.DTOs.User;

public class UserCompleteDto {
    public int Id { get; set; }
    public string Username { get; set; } = String.Empty;
    public List<CommunitySimpleDto> Communities { get; set; } = [];
    public List<PostSimpleDto> Posts { get; set; } = [];
    public List<CommentDto> Comments { get; set; } = [];
}
