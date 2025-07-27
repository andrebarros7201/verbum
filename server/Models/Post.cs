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
    public string Text { get; set; }

    [Required] public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Relationships
    [Required] public int UserId { get; set; }
    public User User { get; set; }

    [Required] public int CommunityId { get; set; }
    public Community Community { get; set; }

    public List<VotePost> Votes { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();
}