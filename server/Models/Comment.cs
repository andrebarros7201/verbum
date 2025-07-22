using System.ComponentModel.DataAnnotations;

namespace Verbum.API.Models;

public class Comment {
    public int Id { get; set; }
    [Required] public string Content { get; set; }

    [Required] public int UserId { get; set; }
    [Required] public User User { get; set; }

    [Required] public int PostId { get; set; }
    [Required] public Post Post { get; set; }

    public List<Vote> Votes { get; set; } = new();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }
}