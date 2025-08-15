using Verbum.API.DTOs.Community;
using Verbum.API.DTOs.Post;
using Verbum.API.DTOs.User;
using Verbum.API.Interfaces;
using Verbum.API.Interfaces.Repositories;
using Verbum.API.Interfaces.Services;
using Verbum.API.Models;
using Verbum.API.Results;

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

    public async Task<ServiceResult<List<CommunitySimpleDto>>> GetCommunities(int userId) {
        List<Community> result = await _communityRepository.GetAllCommunitiesAsync();
        return ServiceResult<List<CommunitySimpleDto>>.Success(
            result.Select(c => new CommunitySimpleDto {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    UserId = c.UserId,
                    MembersCount = c.Members.Count,
                    isMember = c.Members.Any(m => m.UserId == userId),
                    isOwner = c.UserId == userId
                })
                .ToList()
        );
    }

    public async Task<ServiceResult<CommunityCompleteDto>> GetCommunityById(int id, int userId) {
        var result = await _communityRepository.GetCommunityByIdAsync(id);

        // Community not found
        if (result == null) {
            return ServiceResult<CommunityCompleteDto>.Error(ServiceResultStatus.NotFound, "Community not found!");
        }

        return ServiceResult<CommunityCompleteDto>.Success(
            new CommunityCompleteDto {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                Owner = new UserSimpleDto { Id = result.Owner.Id, Username = result.Owner.Username },
                isOwner = result.UserId == userId,
                isMember = result.Members.Any(m => m.UserId == userId),
                isAdmin = result.Members.Any(m => m.UserId == userId && m.IsAdmin),
                Members = result.Members.Select(m => new MemberDto { Id = m.UserId, Username = m.User.Username, isAdmin = m.IsAdmin }).ToList(),
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
            }
        );
    }

    public async Task<ServiceResult<List<CommunitySimpleDto>>> GetCommunitiesByName(string name, int userId) {
        List<Community> result = await _communityRepository.GetCommunitiesByNameAsync(name);
        return ServiceResult<List<CommunitySimpleDto>>.Success(
            result.Select(c => new CommunitySimpleDto {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    UserId = c.UserId,
                    MembersCount = c.Members.Count,
                    isMember = userId == null ? false : c.Members.Any(m => m.UserId == userId),
                    isOwner = c.UserId == userId
                })
                .ToList()
        );
    }

    public async Task<ServiceResult<CommunitySimpleDto>> CreateCommunity(CreateCommunityDto dto, int userId) {
        List<Community> exists = await _communityRepository.GetCommunitiesByNameAsync(dto.Name);

        // Community already exists
        if (exists.Count > 0) {
            return ServiceResult<CommunitySimpleDto>.Error(ServiceResultStatus.Conflict, "Community already exists!");
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
        await JoinCommunity(community.Id, userId); // Add the owner as a member
        await UpdateRole(userId, userId, community.Id); // Add the owner as admin
        return ServiceResult<CommunitySimpleDto>.Success(
            new CommunitySimpleDto {
                Id = community.Id,
                Name = community.Name,
                Description = community.Description,
                UserId = community.UserId,
                MembersCount = community.Members.Count,
                isOwner = true,
                isMember = true
            }
        );
    }

    public async Task<ServiceResult<CommunitySimpleDto>> UpdateCommunity(int userId, int communityId, UpdateCommunityDto dto) {
        var community = await _communityRepository.GetCommunityByIdAsync(communityId);
        if (community == null) {
            return ServiceResult<CommunitySimpleDto>.Error(ServiceResultStatus.NotFound, "Community not found!");
        }

        bool isAdminOrOwner = community.Members.Any(m => m.UserId == userId) || community.UserId == userId;
        if (!isAdminOrOwner) {
            return ServiceResult<CommunitySimpleDto>.Error(ServiceResultStatus.Unauthorized, "Only the Owner or Admin can update the community!");
        }

        community.Name = dto.Name;
        community.Description = dto.Description;

        await _communityRepository.UpdateAsync(community);

        return ServiceResult<CommunitySimpleDto>.Success(
            new CommunitySimpleDto {
                Id = community.Id,
                Name = community.Name,
                Description = community.Description,
                UserId = community.UserId,
                MembersCount = community.Members.Count,
                isMember = community.Members.Any(m => m.UserId == userId),
                isOwner = community.UserId == userId
            });
    }

    public async Task<ServiceResult<bool>> DeleteCommunity(int communityId, int ownerId) {
        var community = await _communityRepository.GetCommunityByIdAsync(communityId);
        if (community == null) {
            return ServiceResult<bool>.Error(ServiceResultStatus.NotFound, "Community not found!");
        }

        if (community.UserId != ownerId) {
            return ServiceResult<bool>.Error(ServiceResultStatus.Unauthorized, "Only the Owner can delete the community!");
        }

        bool result = await _communityRepository.DeleteAsync(communityId);

        return result ? ServiceResult<bool>.Success(true) : ServiceResult<bool>.Error(ServiceResultStatus.Error, "Error deleting community!");
    }

    public async Task<ServiceResult<bool>> JoinCommunity(int communityId, int userId) {
        var community = await _communityRepository.GetCommunityByIdAsync(communityId);
        if (community == null) {
            return ServiceResult<bool>.Error(ServiceResultStatus.NotFound, "Community not found!");
        }

        var uc = await _userCommunityRepository.GetUserCommunity(userId, communityId);
        // User is already a member
        if (uc != null) {
            return ServiceResult<bool>.Error(ServiceResultStatus.Error, "User already a member!");
        }

        var newUc = new UserCommunity {
            UserId = userId,
            CommunityId = communityId
        };

        await _userCommunityRepository.AddUserToCommunity(newUc);
        return ServiceResult<bool>.Success(true);
        ;
    }

    public async Task<ServiceResult<bool>> LeaveCommunity(int communityId, int userId) {
        var community = await _communityRepository.GetCommunityByIdAsync(communityId);
        if (community == null) {
            return ServiceResult<bool>.Error(ServiceResultStatus.NotFound, "Community not found!");
        }

        var uc = await _userCommunityRepository.GetUserCommunity(userId, communityId);

        // User is not a member
        if (uc == null) {
            return ServiceResult<bool>.Error(ServiceResultStatus.Error, "User is not a member!");
        }

        // Owner cannot leave the community
        if (community.UserId == uc.UserId) {
            return ServiceResult<bool>.Error(ServiceResultStatus.Error, "Owner cannot leave the community!");
        }

        await _userCommunityRepository.RemoveUserFromCommunity(uc);
        return ServiceResult<bool>.Success(true);
    }

    public async Task<ServiceResult<MemberDto>> UpdateRole(int targetId, int currentUserId, int communityId) {
        var targetUser = await _userRepository.GetUserByIdAsync(targetId);
        if (targetUser == null) {
            return ServiceResult<MemberDto>.Error(ServiceResultStatus.NotFound, "Target User not found!");
        }

        var currentUser = await _userRepository.GetUserByIdAsync(currentUserId);
        if (currentUser == null) {
            return ServiceResult<MemberDto>.Error(ServiceResultStatus.NotFound, "Current User not found!");
        }

        var community = await _communityRepository.GetCommunityByIdAsync(communityId);
        if (community == null) {
            return ServiceResult<MemberDto>.Error(ServiceResultStatus.NotFound, "Community not found!");
        }

        var currentUserCommunity = await _userCommunityRepository.GetUserCommunity(currentUserId, communityId);
        if (currentUserCommunity == null) {
            return ServiceResult<MemberDto>.Error(ServiceResultStatus.NotFound, "Current User not a member of the community!");
        }

        // User is not the owner or admin
        if (!currentUserCommunity.IsAdmin && currentUser.Id != community.UserId) {
            return ServiceResult<MemberDto>.Error(ServiceResultStatus.Unauthorized, "Only the Owner or Admin can update the role!");
        }

        var targetUserCommunity = await _userCommunityRepository.GetUserCommunity(targetId, communityId);
        if (targetUserCommunity == null) {
            return ServiceResult<MemberDto>.Error(ServiceResultStatus.NotFound, "Target User not a member of the community!");
        }

        targetUserCommunity.IsAdmin = !targetUserCommunity.IsAdmin;
        await _userCommunityRepository.UpdateUserCommunity(targetUserCommunity);

        return ServiceResult<MemberDto>.Success(new MemberDto
            { Id = targetUserCommunity.UserId, Username = targetUserCommunity.User.Username, isAdmin = targetUserCommunity.IsAdmin });
    }
}