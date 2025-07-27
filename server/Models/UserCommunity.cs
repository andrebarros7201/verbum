using System.ComponentModel.DataAnnotations;

namespace Verbum.API.Models;

public class UserCommunity {
    [Required] public int UserId { get; set; }
    [Required] public User User { get; set; }

    [Required] public int CommunityId { get; set; }
    [Required] public Community Community { get; set; }

    [Required] public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsAdmin { get; set; } = false;
}