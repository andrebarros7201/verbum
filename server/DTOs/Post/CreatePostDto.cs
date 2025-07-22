using System.ComponentModel.DataAnnotations;

namespace Verbum.API.DTOs.Post;

public class CreatePostDto {
    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    public string Title { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(500)]
    public string Text { get; set; }

    public int CommunityId { get; set; }
}