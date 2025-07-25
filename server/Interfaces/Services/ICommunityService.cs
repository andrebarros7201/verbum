using Verbum.API.DTOs.Community;

namespace Verbum.API.Interfaces.Services;

public interface ICommunityService {
    Task<CommunityDto> GetCommunityById(int id);
    Task<List<CommunityDto>> GetCommunities();
    Task<List<CommunityDto>> GetCommunitiesByName(string name);
    Task<CommunityDto> CreateCommunity(CreateCommunityDto dto);
    Task<CommunityDto> UpdateCommunity(UpdateCommunityDto dto);
    Task<bool> DeleteCommunity(int id);
}