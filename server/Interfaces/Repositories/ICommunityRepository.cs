using Verbum.API.Models;

namespace Verbum.API.Interfaces.Repositories;

public interface ICommunityRepository {
    Task<List<Community>> GetAllCommunitiesAsync();
    Task<Community?> GetCommunityByIdAsync(int id);
    Task<List<Community>> GetCommunitiesByNameAsync(string name);
    Task<Community> AddAsync(Community community);
    Task<Community> UpdateAsync(Community community);
    Task<bool> DeleteAsync(int id);
}