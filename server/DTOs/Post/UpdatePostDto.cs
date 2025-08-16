using System.ComponentModel.DataAnnotations;

namespace Verbum.API.DTOs.Post;

public class UpdatePostDto {
    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    public string Title { get; set; } = String.Empty;

    [Required]
    [MinLength(3)]
    [MaxLength(500)]
    public string Text { get; set; } = String.Empty;
}
