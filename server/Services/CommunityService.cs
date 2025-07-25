using Verbum.API.DTOs.Community;
using Verbum.API.Interfaces.Repositories;
using Verbum.API.Interfaces.Services;
using Verbum.API.Models;

namespace Verbum.API.Services;

public class CommunityService : ICommunityService {
    private readonly ICommunityRepository _communityRepository;
    private readonly IUserRepository _userRepository;

    public CommunityService(ICommunityRepository communityRepository, IUserRepository userRepository) {
        _communityRepository = communityRepository;
        _userRepository = userRepository;
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

    public Task<CommunityDto> UpdateCommunity(UpdateCommunityDto dto) {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteCommunity(int id) {
        throw new NotImplementedException();
    }

    public async Task<CommunityDto> CreateCommunity(CreateCommunityDto dto, int userId) {
        List<Community> exists = await _communityRepository.GetCommunitiesByNameAsync(dto.Name);
        if (exists.Count > 0) {
            return null; // Community already exists
        }

        var user = await _userRepository.GetUserByIdAsync(userId);

        if (user == null) {
            return null;
        }

        var community = new Community {
            Name = dto.Name,
            Description = dto.Description,
            UserId = userId
        };

        await _communityRepository.AddAsync(community);
        return new CommunityDto { Id = community.Id, Name = community.Name, Description = community.Description };
    }
}