using Verbum.API.DTOs.Post;
using Verbum.API.DTOs.User;

namespace Verbum.API.DTOs.Community;

public class CommunityCompleteDto {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public UserSimpleDto Owner { get; set; }
    public List<PostSimpleDto> Posts { get; set; }
    public List<MemberDto> Members { get; set; }
    public int MembersCount { get; set; }
    public int PostsCount { get; set; }
    public bool isMember { get; set; }
    public bool isOwner { get; set; }
    public bool isAdmin { get; set; }
}