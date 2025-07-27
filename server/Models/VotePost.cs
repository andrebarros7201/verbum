using System.ComponentModel.DataAnnotations;

namespace Verbum.API.Models;

public class VotePost {
    public int Id { get; set; }

    [Required] public int UserId { get; set; }
    public User User { get; set; }

    [Required] public int PostId { get; set; }
    public Post Post { get; set; }

    [Required] [Range(-1, 1)] public int Value { get; set; }
}