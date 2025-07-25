using Verbum.API.DTOs.User;

namespace Verbum.API.DTOs.Community;

public class CommunityDto {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<UserDto> Members { get; set; }
}