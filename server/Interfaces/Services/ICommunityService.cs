using Verbum.API.DTOs.Community;

namespace Verbum.API.Interfaces.Services;

public interface ICommunityService {
    Task<CommunityCompleteDto?> GetCommunityById(int id);
    Task<List<CommunitySimpleDto>> GetCommunities();
    Task<List<CommunitySimpleDto>> GetCommunitiesByName(string name);
    Task<CommunitySimpleDto?> CreateCommunity(CreateCommunityDto dto, int userId);
    Task<CommunitySimpleDto?> UpdateCommunity(int userId, int communityId, UpdateCommunityDto dto);
    Task<bool> JoinCommunity(int communityId, int userId);
    Task<bool> LeaveCommunity(int communityId, int userId);
    Task<bool> DeleteCommunity(int communityId, int userId);
}