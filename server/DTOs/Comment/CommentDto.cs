using Verbum.API.DTOs.User;

namespace Verbum.API.DTOs.Comment;

public class CommentDto {
    public int Id { get; set; }
    public string Text { get; set; } = String.Empty;
    public UserSimpleDto? Author { get; set; }
    public int Votes { get; set; } = 0;
    public int PostId { get; set; } = 0;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
