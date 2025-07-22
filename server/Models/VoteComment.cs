using System.ComponentModel.DataAnnotations;

namespace Verbum.API.Models;

public class VoteComment {
    public int Id { get; set; }

    [Required] public int UserId { get; set; }
    [Required] public User User { get; set; }

    [Required] public int CommentId { get; set; }
    [Required] public Comment Comment { get; set; }

    [Range(-1, 1)] public int Value { get; set; }
}