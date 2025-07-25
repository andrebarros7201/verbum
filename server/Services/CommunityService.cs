using Verbum.API.DTOs.Community;
using Verbum.API.DTOs.User;
using Verbum.API.Interfaces;
using Verbum.API.Interfaces.Repositories;
using Verbum.API.Interfaces.Services;
using Verbum.API.Models;

namespace Verbum.API.Services;

public class CommunityService : ICommunityService {
    private readonly ICommunityRepository _communityRepository;
    private readonly IUserCommunityRepository _userCommunityRepository;
    private readonly IUserRepository _userRepository;

    public CommunityService(ICommunityRepository communityRepository, IUserRepository userRepository,
        IUserCommunityRepository userCommunityRepository) {
        _communityRepository = communityRepository;
        _userRepository = userRepository;
        _userCommunityRepository = userCommunityRepository;
    }

    public async Task<CommunityDto> GetCommunityById(int id) {
        var result = await _communityRepository.GetCommunityByIdAsync(id);
        return new CommunityDto {
            Id = result.Id, Name = result.Name, Description = result.Description, UserId = result.UserId,
            Members = result.Members.Select(m => new UserDto { Id = m.UserId, Username = m.User.Username }).ToList()
        };
    }

    public async Task<List<CommunityDto>> GetCommunities() {
        List<Community> result = await _communityRepository.GetAllCommunitiesAsync();
        return result.Select(c => new CommunityDto {
            Id = c.Id, Name = c.Name, Description = c.Description, UserId = c.UserId,
            Members = c.Members.Select(m => new UserDto { Id = m.UserId, Username = m.User.Username }).ToList()
        }).ToList();
    }

    public async Task<List<CommunityDto>> GetCommunitiesByName(string name) {
        List<Community> result = await _communityRepository.GetCommunitiesByNameAsync(name);
        return result.Select(c => new CommunityDto {
            Id = c.Id, Name = c.Name, Description = c.Description, UserId = c.UserId,
            Members = c.Members.Select(m => new UserDto { Id = m.UserId, Username = m.User.Username }).ToList()
        }).ToList();
    }

    public Task<CommunityDto> UpdateCommunity(UpdateCommunityDto dto) {
        throw new NotImplementedException();
    }

    public async Task<bool> JoinCommunity(int communityId, int userId) {
        var community = await _communityRepository.GetCommunityByIdAsync(communityId);
        if (community == null) {
            return false;
        }

        var uc = await _userCommunityRepository.GetUserCommunity(userId, communityId);
        // User is already a member
        if (uc != null) {
            return true;
        }

        var newUc = new UserCommunity {
            UserId = userId,
            CommunityId = communityId
        };

        await _userCommunityRepository.AddUserToCommunity(newUc);
        return true;
    }

    public async Task<bool> LeaveCommunity(int communityId, int userId) {
        var community = await _communityRepository.GetCommunityByIdAsync(communityId);
        if (community == null) {
            return false;
        }

        var uc = await _userCommunityRepository.GetUserCommunity(userId, communityId);
        // User is not a member
        if (uc == null) {
            return true;
        }

        await _userCommunityRepository.RemoveUserFromCommunity(uc);
        return true;
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
            UserId = userId,
            Owner = user
        };

        await _communityRepository.AddAsync(community);
        return new CommunityDto { Id = community.Id, Name = community.Name, Description = community.Description };
    }

    public async Task<bool> DeleteCommunity(int communityId, int ownerId) {
        var community = await _communityRepository.GetCommunityByIdAsync(communityId);
        if (community == null) {
            return false;
        }

        if (community.UserId != ownerId) {
            return false;
        }

        bool result = await _communityRepository.DeleteAsync(communityId);

        return result;
    }
}