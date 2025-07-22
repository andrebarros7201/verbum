using Verbum.API.DTOs.Community;

namespace Verbum.API.DTOs.User.UserContent;

public class UserCommunitiesDto {
    public int Id { get; set; }
    public List<CommunityDto> Communities { get; set; }
}