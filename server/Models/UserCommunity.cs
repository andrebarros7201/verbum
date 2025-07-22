namespace Verbum.API.Models;

public class UserCommunity {
    public int UserId { get; set; }
    public User User { get; set; }

    public int CommunityId { get; set; }
    public Community Community { get; set; }

    public bool IsAdmin { get; set; }
}