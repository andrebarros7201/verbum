using Verbum.API.DTOs.Community;
using Verbum.API.Interfaces.Repositories;
using Verbum.API.Interfaces.Services;
using Verbum.API.Models;

namespace Verbum.API.Services;

public class CommunityService : ICommunityService {
    private readonly ICommunityRepository _communityRepository;

    public CommunityService(ICommunityRepository communityRepository) {
        _communityRepository = communityRepository;
    }

    public async Task<CommunityDto> GetCommunityById(int id) {
        var result = await _communityRepository.GetCommunityByIdAsync(id);
        return new CommunityDto { Id = result.Id, Name = result.Name, Description = result.Description };
    }

    public async Task<List<CommunityDto>> GetCommunities() {
        List<Community> result = await _communityRepository.GetAllCommunitiesAsync();
        return result.Select(c => new CommunityDto { Id = c.Id, Name = c.Name, Description = c.Description }).ToList();
    }

    public async Task<List<CommunityDto>> GetCommunitiesByName(string name) {
        List<Community> result = await _communityRepository.GetCommunitiesByNameAsync(name);
        return result.Select(c => new CommunityDto { Id = c.Id, Name = c.Name, Description = c.Description }).ToList();
    }

    public Task<CommunityDto> CreateCommunity(CreateCommunityDto dto) {
        throw new NotImplementedException();
    }

    public Task<CommunityDto> UpdateCommunity(UpdateCommunityDto dto) {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteCommunity(int id) {
        throw new NotImplementedException();
    }
}