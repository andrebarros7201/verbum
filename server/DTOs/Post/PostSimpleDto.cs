using Verbum.API.DTOs.Community;
using Verbum.API.DTOs.User;

namespace Verbum.API.DTOs.Post;

public class PostSimpleDto {
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Votes { get; set; }
    public int CommentsCount { get; set; }
    public DateTime Created { get; set; }
    public UserSimpleDto UserSimple { get; set; }
    public CommunitySimpleDto Community { get; set; }
}