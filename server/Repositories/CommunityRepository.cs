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
        return await _db.Communities.FindAsync(id);
    }

    public async Task<List<Community>> GetAllCommunitiesAsync() {
        return await _db.Communities.ToListAsync();
    }

    public async Task<List<Community>> GetCommunitiesByNameAsync(string name) {
        IQueryable<Community> result = _db.Communities.Where(c => c.Name.Contains(name));
        return await result.ToListAsync();
    }

    public Task<Community> AddAsync(Community community) {
        throw new NotImplementedException();
    }

    public Task<Community> UpdateAsync(Community community) {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id) {
        throw new NotImplementedException();
    }
}