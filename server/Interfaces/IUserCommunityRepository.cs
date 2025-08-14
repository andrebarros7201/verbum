using Verbum.API.Models;

namespace Verbum.API.Interfaces;

public interface IUserCommunityRepository {
    Task<UserCommunity?> GetUserCommunity(int userId, int communityId);
    Task<bool> AddUserToCommunity(UserCommunity uc);
    Task<bool> RemoveUserFromCommunity(UserCommunity uc);
    Task<bool> UpdateUserCommunity(UserCommunity uc);
}