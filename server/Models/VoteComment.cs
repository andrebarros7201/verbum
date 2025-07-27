using System.ComponentModel.DataAnnotations;

namespace Verbum.API.Models;

public class VoteComment {
    public int Id { get; set; }

    [Required] public int UserId { get; set; }
    public User User { get; set; }

    [Required] public int CommentId { get; set; }
    public Comment Comment { get; set; }

    [Required] [Range(-1, 1)] public int Value { get; set; }
}