using Verbum.API.DTOs.User;

namespace Verbum.API.DTOs.Comment;

public class CommentDto {
    public int Id { get; set; }
    public string Text { get; set; }
    public UserDto Author { get; set; }
    public int Votes { get; set; }
    public DateTime Created { get; set; }
}