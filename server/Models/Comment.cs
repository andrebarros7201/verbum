using System.ComponentModel.DataAnnotations;

namespace Verbum.API.Models;

public class Comment {
    public int Id { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(500)]
    public string Text { get; set; }

    [Required] public int UserId { get; set; }
    public User User { get; set; }

    [Required] public int PostId { get; set; }
    public Post Post { get; set; }

    public List<VoteComment> Votes { get; set; } = new();
    [Required] public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}