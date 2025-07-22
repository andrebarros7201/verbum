using System.ComponentModel.DataAnnotations;

namespace Verbum.API.Models;

public class Vote {
    public int Id { get; set; }

    [Required] public int UserId { get; set; }
    [Required] public User User { get; set; }

    public int? PostId { get; set; }
    public Post Post { get; set; }

    public int? CommentId { get; set; }
    public Comment Comment { get; set; }

    [Range(-1, 1)] public int Value { get; set; }
}