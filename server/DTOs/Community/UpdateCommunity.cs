using System.ComponentModel.DataAnnotations;

namespace Verbum.API.DTOs.Community;

public class UpdateCommunity {
    [Required]
    [MinLength(3)]
    [MaxLength(20)]
    public string Name { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(300)]
    public string Description { get; set; }
}