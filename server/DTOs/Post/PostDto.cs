using Verbum.API.DTOs.Community;
using Verbum.API.DTOs.User;

namespace Verbum.API.DTOs.Post;

public class PostDto {
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public UserDto User { get; set; }
    public CommunityDto Community { get; set; }
}