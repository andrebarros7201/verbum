using System.ComponentModel.DataAnnotations;

namespace Verbum.API.DTOs.Community;

public class UpdateCommunityDto {
    [Required]
    [MinLength(3)]
    [MaxLength(20)]
    public string Name { get; set; } = String.Empty;

    [Required]
    [MinLength(3)]
    [MaxLength(300)]
    public string Description { get; set; } = String.Empty;
}
