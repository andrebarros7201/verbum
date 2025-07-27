using Verbum.API.DTOs.Community;

namespace Verbum.API.Interfaces.Services;

public interface ICommunityService {
    Task<CommunityDto> GetCommunityById(int id);
    Task<List<CommunityDto>> GetCommunities();
    Task<List<CommunityDto>> GetCommunitiesByName(string name);
    Task<CommunityDto> CreateCommunity(CreateCommunityDto dto, int userId);
    Task<CommunityDto> UpdateCommunity(int userId, int communityId, UpdateCommunityDto dto);
    Task<bool> JoinCommunity(int communityId, int userId);
    Task<bool> LeaveCommunity(int communityId, int userId);
    Task<bool> DeleteCommunity(int communityId, int userId);
}