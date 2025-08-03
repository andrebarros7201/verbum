using Microsoft.EntityFrameworkCore;
using Verbum.API.Data;
using Verbum.API.Interfaces.Repositories;
using Verbum.API.Models;

namespace Verbum.API.Repositories;

public class CommunityRepository : ICommunityRepository {
    private readonly AppDbContext _db;

    public CommunityRepository(AppDbContext db) {
        _db = db;
    }

    /// <summary>
    ///     Finds the community by its ID
    /// </summary>
    /// <param name="id">Community ID</param>
    /// <returns>Returns the community (model) or null.</returns>
    public async Task<Community?> GetCommunityByIdAsync(int id) {
        return await _db.Communities
            .Where(c => c.Id == id)
            .Include(c => c.Members)
            .ThenInclude(m => m.User)
            .Include(c => c.Posts)
            .ThenInclude(p => p.Votes)
            .Include(c => c.Posts)
            .ThenInclude(p => p.User)
            .FirstOrDefaultAsync();
    }

    /// <summary>
    ///     Fetch all the communities
    /// </summary>
    /// <returns>Returns a list with all the communities available. Never returns null. Can return a list of length 0.</returns>
    public async Task<List<Community>> GetAllCommunitiesAsync() {
        return await _db.Communities
            .Include(c => c.Members)
            .ThenInclude(m => m.User)
            .ToListAsync();
    }

    /// <summary>
    ///     Finds all communities that contain the name provided in the argument
    /// </summary>
    /// <param name="name">Name to find</param>
    /// <returns>
    ///     Returns a list of all the communities that contain the name provided. Can return an empty list if there was
    ///     none.
    /// </returns>
    public async Task<List<Community>> GetCommunitiesByNameAsync(string name) {
        return await _db.Communities
            .Where(c => c.Name.ToLower().Contains(name.Trim().ToLower()))
            .Include(c => c.Members)
            .ThenInclude(m => m.User)
            .ToListAsync();
    }

    public async Task<Community> AddAsync(Community community) {
        await _db.Communities.AddAsync(community);
        await _db.SaveChangesAsync();
        return community;
    }

    public async Task<Community> UpdateAsync(Community community) {
        _db.Communities.Update(community);
        await _db.SaveChangesAsync();
        return community;
    }

    public async Task<bool> DeleteAsync(int id) {
        var community = await _db.Communities.FindAsync(id);
        if (community == null) {
            return false;
        }

        _db.Communities.Remove(community);
        await _db.SaveChangesAsync();
        return true;
    }
}