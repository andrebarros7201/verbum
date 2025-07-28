using Verbum.API.DTOs.Community;
using Verbum.API.DTOs.Post;
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

        // Owner cannot leave the community
        if (community.UserId == uc.UserId) {
            return false;
        }

        await _userCommunityRepository.RemoveUserFromCommunity(uc);
        return true;
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

    public async Task<CommunitySimpleDto?> UpdateCommunity(int userId, int communityId, UpdateCommunityDto dto) {
        var community = await _communityRepository.GetCommunityByIdAsync(communityId);
        if (community == null) {
            return null;
        }

        bool isAdminOrOwner = community.Members.Any(m => m.UserId == userId) || community.UserId == userId;
        if (!isAdminOrOwner) {
            return null;
        }

        community.Name = dto.Name;
        community.Description = dto.Description;

        await _communityRepository.UpdateAsync(community);

        return new CommunitySimpleDto {
            Id = community.Id,
            Name = community.Name,
            Description = community.Description,
            UserId = community.UserId,
            MembersCount = community.Members.Count,
            isMember = community.Members.Any(m => m.UserId == userId),
            isOwner = community.UserId == userId
        };
    }

    /// <summary>
    ///     Finds all the communities where its name contains the name received as argument
    /// </summary>
    /// <param name="name">Community Name</param>
    /// <returns>Returns a list of Community DTO</returns>
    public async Task<List<CommunitySimpleDto>> GetCommunitiesByName(string name, int userId) {
        List<Community> result = await _communityRepository.GetCommunitiesByNameAsync(name);
        return result.Select(c => new CommunitySimpleDto {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            UserId = c.UserId,
            MembersCount = c.Members.Count,
            isMember = userId == null ? false : c.Members.Any(m => m.UserId == userId),
            isOwner = c.UserId == userId
        }).ToList();
    }

    public async Task<CommunitySimpleDto?> CreateCommunity(CreateCommunityDto dto, int userId) {
        List<Community> exists = await _communityRepository.GetCommunitiesByNameAsync(dto.Name);
        if (exists.Count > 0) {
            return null; // Community already exists
        }

        var user = await _userRepository.GetUserByIdAsync(userId);

        var community = new Community {
            Name = dto.Name,
            Description = dto.Description,
            UserId = userId,
            Owner = user,
            Members = []
        };

        await _communityRepository.AddAsync(community);
        await JoinCommunity(community.Id, userId);
        return new CommunitySimpleDto {
            Id = community.Id,
            Name = community.Name,
            Description = community.Description,
            UserId = community.UserId,
            MembersCount = community.Members.Count,
            isOwner = true,
            isMember = true
        };
    }

    /// <summary>
    ///     Find a Community based on received ID
    /// </summary>
    /// <param name="id">Community ID</param>
    /// <param name="userId">User Id</param>
    /// <returns>Returns a community dto or null if it didn't find a community</returns>
    public async Task<CommunityCompleteDto?> GetCommunityById(int id, int userId) {
        var result = await _communityRepository.GetCommunityByIdAsync(id);
        if (result == null) {
            return null;
        }

        return new CommunityCompleteDto {
            Id = result.Id,
            Name = result.Name,
            Description = result.Description,
            isOwner = result.UserId == userId,
            isMember = result.Members.Any(m => m.UserId == userId),
            MembersCount = result.Members.Count,
            Posts = result.Posts
                .Select(p => new PostSimpleDto {
                    Id = p.Id,
                    CommentsCount = p.Comments.Count,
                    Created = p.CreatedAt,
                    Title = p.Title,
                    CommunityId = p.CommunityId,
                    User = new UserSimpleDto { Id = p.User.Id, Username = p.User.Username },
                    Votes = p?.Votes?.Aggregate(0, (acc, curr) => acc + curr.Value) ?? 0
                })
                .ToList(),
            PostsCount = result.Posts.Count
        };
    }

    /// <summary>
    ///     Fetch all the communities as a list of Community DTO
    /// </summary>
    /// <returns>A list of Community DTO</returns>
    public async Task<List<CommunitySimpleDto>> GetCommunities(int userId) {
        List<Community> result = await _communityRepository.GetAllCommunitiesAsync();
        return result.Select(c => new CommunitySimpleDto {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            UserId = c.UserId,
            MembersCount = c.Members.Count,
            isMember = c.Members.Any(m => m.UserId == userId),
            isOwner = c.UserId == userId
        }).ToList();
    }
}