using Verbum.API.DTOs.Comment;
using Verbum.API.DTOs.Community;
using Verbum.API.DTOs.User;

namespace Verbum.API.DTOs.Post;

public class PostCompleteDto {
    public int Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public string Text { get; set; } = String.Empty;
    public UserSimpleDto User { get; set; } = null!;
    public int Votes { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public CommunitySimpleDto Community { get; set; } = null!;
    public List<CommentDto> Comments { get; set; } = [];
    public int CommentsCount { get; set; }
}
