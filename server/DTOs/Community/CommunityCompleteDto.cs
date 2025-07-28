using Verbum.API.DTOs.Post;

namespace Verbum.API.DTOs.Community;

public class CommunityCompleteDto {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<PostSimpleDto> Posts { get; set; }
    public int MembersCount { get; set; }
    public int PostsCount { get; set; }
    public bool isMember { get; set; }
    public bool isOwner { get; set; }
}