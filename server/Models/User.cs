using System.ComponentModel.DataAnnotations;

namespace Verbum.API.Models;

public class User {
    public int Id { get; set; }
    [Required] [MinLength(3)] public string Username { get; set; } = string.Empty;
    [Required] [MinLength(6)] public string Password { get; set; } = string.Empty;
    [Required] public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Relationships
    public List<UserCommunity> CommunitiesJoined { get; set; } = new();
    public List<Post> Posts { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();
    public List<VotePost> VotePosts { get; set; } = new();
    public List<VoteComment> VoteComments { get; set; } = new();
}