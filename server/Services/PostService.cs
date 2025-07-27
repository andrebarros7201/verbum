using Verbum.API.DTOs.Comment;
using Verbum.API.DTOs.Community;
using Verbum.API.DTOs.Post;
using Verbum.API.DTOs.User;
using Verbum.API.Interfaces;
using Verbum.API.Interfaces.Repositories;
using Verbum.API.Interfaces.Services;
using Verbum.API.Models;

namespace Verbum.API.Services;

public class PostService : IPostService {
    private readonly ICommunityRepository _communityRepository;
    private readonly IPostRepository _postRepository;
    private readonly IUserCommunityRepository _userCommunityRepository;
    private readonly IUserRepository _userRepository;

    public PostService(IPostRepository postRepository, IUserRepository userRepository, ICommunityRepository communityRepository,
        IUserCommunityRepository userCommunityRepository) {
        _postRepository = postRepository;
        _userRepository = userRepository;
        _communityRepository = communityRepository;
        _userCommunityRepository = userCommunityRepository;
    }

    public async Task<PostCompleteDto?> GetPostById(int id) {
        var post = await _postRepository.GetPostByIdAsync(id);
        if (post == null) {
            return null;
        }

        return new PostCompleteDto {
            Id = post.Id,
            Title = post.Title,
            Text = post.Text,
            Created = post.CreatedAt,
            Updated = post.UpdatedAt,
            User = new UserSimpleDto { Id = post.User.Id, Username = post.User.Username },
            Votes = post.Votes != null ? post.Votes.Aggregate(0, (acc, curr) => acc + curr.Value) : 0,
            Community = new CommunitySimpleDto {
                Id = post.Community.Id, Name = post.Community.Name, Description = post.Community.Description,
                MembersCount = post.Community.Members.Count, UserId = post.Community.UserId
            },
            CommentsCount = post.Comments.Count,
            Comments = post.Comments.Select(c => new CommentDto {
                Id = c.Id,
                Author = new UserSimpleDto { Id = c.User.Id, Username = c.User.Username },
                CreatedAt = c.CreatedAt,
                Text = c.Text,
                UpdatedAt = c.UpdatedAt,
                Votes = c.Votes.Aggregate(0, (acc, curr) => acc + curr.Value)
            }).ToList()
        };
    }

    public Task<List<PostSimpleDto>> GetPostsByCommunityId(int communityId) {
        throw new NotImplementedException();
    }

    public async Task<PostSimpleDto> CreatePost(CreatePostDto dto, int userId) {
        var user = await _userRepository.GetUserByIdAsync(userId);
        var community = await _communityRepository.GetCommunityByIdAsync(dto.CommunityId);
        if (user == null || community == null) {
            return null;
        }

        // User must be a member to create a post
        bool userIsMember = await _userCommunityRepository.GetUserCommunity(userId, community.Id) != null;
        if (!userIsMember) {
            return null;
        }

        var post = new Post {
            Title = dto.Title,
            Text = dto.Text,
            UserId = userId,
            User = user,
            Community = community,
            CommunityId = dto.CommunityId,
            Comments = new List<Comment>()
        };

        var userDto = new UserSimpleDto { Id = user.Id, Username = user.Username };
        var communityDto = new CommunitySimpleDto {
            Id = community.Id,
            Name = community.Name,
            Description = community.Description,
            UserId = community.UserId,
            MembersCount = community.Members.Count
        };

        await _postRepository.AddAsync(post);

        return new PostSimpleDto {
            Id = post.Id,
            Title = post.Title,
            Created = post.CreatedAt,
            User = userDto,
            CommentsCount = post.Comments.Count,
            Votes = post.Votes != null ? post.Votes.Aggregate(0, (acc, curr) => acc + curr.Value) : 0,
            Community = communityDto
        };
    }

    public Task<PostSimpleDto> UpdatePost(int userId, int postId, UpdatePostDto dto) {
        throw new NotImplementedException();
    }

    public async Task<bool> DeletePost(int userId, int postId) {
        var user = await _userRepository.GetUserByIdAsync(userId);
        var post = await _postRepository.GetPostByIdAsync(postId);
        if (user == null || post == null) {
            return false;
        }

        if (user.Id != post.UserId) {
            return false;
        }

        return await _postRepository.DeleteAsync(postId);
    }
}