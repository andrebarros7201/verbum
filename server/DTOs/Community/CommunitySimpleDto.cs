namespace Verbum.API.DTOs.Community;

public class CommunitySimpleDto {
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public int UserId { get; set; }
    public int MembersCount { get; set; }
    public bool isMember { get; set; }
    public bool isOwner { get; set; }
}
