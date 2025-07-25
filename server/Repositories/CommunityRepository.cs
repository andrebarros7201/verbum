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


    public async Task<Community> GetCommunityByIdAsync(int id) {
        return await _db.Communities.Include(c => c.Members).ThenInclude(m => m.User).FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Community>> GetAllCommunitiesAsync() {
        return await _db.Communities.Include(c => c.Members).ThenInclude(m => m.User).ToListAsync();
    }

    public async Task<List<Community>> GetCommunitiesByNameAsync(string name) {
        IQueryable<Community> result = _db.Communities.Where(c => c.Name.Contains(name)).Include(c => c.Members).ThenInclude(m => m.User);
        return await result.ToListAsync();
    }

    public async Task<Community> AddAsync(Community community) {
        await _db.Communities.AddAsync(community);
        await _db.SaveChangesAsync();
        return community;
    }

    public Task<Community> UpdateAsync(Community community) {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id) {
        throw new NotImplementedException();
    }
}