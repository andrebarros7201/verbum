using System.ComponentModel.DataAnnotations;

namespace Verbum.API.Models;

public class Community {
    public int Id { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(20)]
    public string Name { get; set; }

    [Required] [StringLength(300)] public string Description { get; set; }
    [Required] public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

     public int UserId { get; set; }
     public User Owner { get; set; }

    // Relationships
    public List<UserCommunity> Members { get; set; } = new();
    public List<Post> Posts { get; set; } = new();
}