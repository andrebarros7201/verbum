using Verbum.API.DTOs.Comment;
using Verbum.API.DTOs.Community;
using Verbum.API.DTOs.User;

namespace Verbum.API.DTOs.Post;

public class PostCompleteDto {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public UserSimpleDto UserSimple { get; set; }
    public int Votes { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public CommunitySimpleDto Community { get; set; }
    public List<CommentDto> Comments { get; set; }
    public int CommentsCount { get; set; }
}