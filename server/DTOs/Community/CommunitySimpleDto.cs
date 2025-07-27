namespace Verbum.API.DTOs.Community;

public class CommunitySimpleDto {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int UserId { get; set; }
    public int MembersCount { get; set; }
}