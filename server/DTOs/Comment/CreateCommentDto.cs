using System.ComponentModel.DataAnnotations;

namespace Verbum.API.DTOs.Comment;

public class CreateCommentDto {
    [Required] [MinLength(1)] public string Text { get; set; }
}