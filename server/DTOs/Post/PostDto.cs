using Verbum.API.DTOs.Comment;
using Verbum.API.DTOs.User;

namespace Verbum.API.DTOs.Post;

public class PostDto {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Created { get; set; }
    public UserDto Author { get; set; }
    public List<CommentDto> Comments { get; set; }
    public int Votes { get; set; }
}