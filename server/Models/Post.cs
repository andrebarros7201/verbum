using System.ComponentModel.DataAnnotations;

namespace Verbum.API.Models;

public class Post {
    public int Id { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    public string Title { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(500)]
    public string Content { get; set; }

    [Required] public int UserId { get; set; }
    [Required] public User User { get; set; }

    [Required] public int CommunityId { get; set; }
    [Required] public Community Community { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Relationships
    public List<Vote> Votes { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();
}