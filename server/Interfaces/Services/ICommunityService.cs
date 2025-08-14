using Verbum.API.DTOs.Community;
using Verbum.API.DTOs.User;
using Verbum.API.Results;

namespace Verbum.API.Interfaces.Services;

public interface ICommunityService {
    Task<ServiceResult<CommunityCompleteDto>> GetCommunityById(int id, int userId);
    Task<ServiceResult<List<CommunitySimpleDto>>> GetCommunities(int userId);
    Task<ServiceResult<List<CommunitySimpleDto>>> GetCommunitiesByName(string name, int userId);
    Task<ServiceResult<CommunitySimpleDto>> CreateCommunity(CreateCommunityDto dto, int userId);
    Task<ServiceResult<CommunitySimpleDto>> UpdateCommunity(int userId, int communityId, UpdateCommunityDto dto);
    Task<ServiceResult<bool>> JoinCommunity(int communityId, int userId);
    Task<ServiceResult<bool>> LeaveCommunity(int communityId, int userId);
    Task<ServiceResult<bool>> DeleteCommunity(int communityId, int userId);
    Task<ServiceResult<MemberDto>> UpdateRole(int targetId, int currentId, int communityId);
}