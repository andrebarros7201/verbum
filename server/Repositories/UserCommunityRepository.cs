using Verbum.API.Data;
using Verbum.API.Interfaces;
using Verbum.API.Models;

namespace Verbum.API.Repositories;

public class UserCommunityRepository : IUserCommunityRepository {

    private readonly AppDbContext _db;

    public UserCommunityRepository(AppDbContext db) {
        _db = db;
    }

    public async Task<UserCommunity> GetUserCommunity(int userId, int communityId) {
        return await _db.UserCommunities.FindAsync(userId, communityId);
    }

    public async Task<bool> AddUserToCommunity(UserCommunity uc) {
        _db.UserCommunities.Add(uc);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveUserFromCommunity(UserCommunity uc) {
        var userCommunity = await _db.UserCommunities.FindAsync(uc.UserId, uc.CommunityId);
        if (userCommunity == null) {
            return false;
        }

        _db.UserCommunities.Remove(userCommunity);
        await _db.SaveChangesAsync();
        return true;
    }
}