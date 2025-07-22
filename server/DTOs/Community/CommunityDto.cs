using Verbum.API.DTOs.Post;
using Verbum.API.DTOs.User;

namespace Verbum.API.DTOs.Community;

public class CommunityDto {
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public List<UserDto> Members { get; set; }
    public int CreatorId { get; set; }
    public List<UserDto> Admins { get; set; }
    public DateTime Created { get; set; }
    public List<PostDto> Posts { get; set; }
}