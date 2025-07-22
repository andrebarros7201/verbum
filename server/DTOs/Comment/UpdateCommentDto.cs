using System.ComponentModel.DataAnnotations;

namespace Verbum.API.DTOs.Comment;

public class UpdateCommentDto {
    [Required]
    [MinLength(3)]
    [MaxLength(500)]
    public string Text { get; set; }
}