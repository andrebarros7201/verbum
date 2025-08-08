using Verbum.API.DTOs.Comment;
using Verbum.API.DTOs.Community;
using Verbum.API.DTOs.Post;
using Verbum.API.DTOs.User;
using Verbum.API.Interfaces;
using Verbum.API.Interfaces.Repositories;
using Verbum.API.Interfaces.Services;
using Verbum.API.Models;
using Verbum.API.Results;

namespace Verbum.API.Services;

public class PostService : IPostService {
    private readonly ICommunityRepository _communityRepository;
    private readonly IPostRepository _postRepository;
    private readonly IUserCommunityRepository _userCommunityRepository;
    private readonly IUserRepository _userRepository;
    private readonly IVotePostRepository _votePostRepository;

    public PostService(IPostRepository postRepository, IUserRepository userRepository, ICommunityRepository communityRepository,
        IUserCommunityRepository userCommunityRepository, IVotePostRepository votePostRepository) {
        _postRepository = postRepository;
        _userRepository = userRepository;
        _communityRepository = communityRepository;
        _userCommunityRepository = userCommunityRepository;
        _votePostRepository = votePostRepository;
    }

    public async Task<ServiceResult<PostCompleteDto>> GetPostById(int id, int userId) {
        var post = await _postRepository.GetPostByIdAsync(id);
        if (post == null) {
            return ServiceResult<PostCompleteDto>.Error(ServiceResultStatus.NotFound, "Post not found!");
        }

        return ServiceResult<PostCompleteDto>.Success(new PostCompleteDto {
                Id = post.Id,
                Title = post.Title,
                Text = post.Text,
                Created = post.CreatedAt,
                Updated = post.UpdatedAt,
                User = new UserSimpleDto { Id = post.User.Id, Username = post.User.Username },
                Votes = post.Votes.Aggregate(0, (acc, curr) => acc + curr.Value),
                Community = new CommunitySimpleDto {
                    Id = post.Community.Id,
                    Name = post.Community.Name,
                    Description = post.Community.Description,
                    MembersCount = post.Community.Members.Count,
                    UserId = post.Community.UserId,
                    isMember = post.Community.Members.Any(m => m.UserId == userId),
                    isOwner = post.Community.UserId == userId
                },
                CommentsCount = post.Comments.Count,
                Comments = post.Comments
                    .Select(c => new CommentDto {
                            Id = c.Id,
                            Author = new UserSimpleDto { Id = c.User.Id, Username = c.User.Username },
                            CreatedAt = c.CreatedAt,
                            Text = c.Text,
                            PostId = c.PostId,
                            UpdatedAt = c.UpdatedAt,
                            Votes = c.Votes.Aggregate(0, (acc, curr) => acc + curr.Value)
                        }
                    )
                    .ToList()
            }
        );
    }

    public async Task<ServiceResult<PostSimpleDto>> CreatePost(CreatePostDto dto, int userId) {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null) {
            return ServiceResult<PostSimpleDto>.Error(ServiceResultStatus.NotFound, "User not found!");
        }

        var community = await _communityRepository.GetCommunityByIdAsync(dto.CommunityId);
        if (community == null) {
            return ServiceResult<PostSimpleDto>.Error(ServiceResultStatus.NotFound, "Community not found!");
        }

        // User must be a member to create a post
        bool userIsMember = await _userCommunityRepository.GetUserCommunity(userId, community.Id) != null;
        if (!userIsMember) {
            return ServiceResult<PostSimpleDto>.Error(ServiceResultStatus.Unauthorized, "User is not a member of the community!");
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

        await _postRepository.AddAsync(post);

        return ServiceResult<PostSimpleDto>.Success(new PostSimpleDto {
                Id = post.Id,
                Title = post.Title,
                Created = post.CreatedAt,
                User = new UserSimpleDto { Id = user.Id, Username = user.Username },
                CommentsCount = post.Comments.Count,
                Votes = post.Votes != null ? post.Votes.Aggregate(0, (acc, curr) => acc + curr.Value) : 0,
                CommunityId = post.CommunityId
            }
        );
    }

    public async Task<ServiceResult<bool>> DeletePost(int userId, int postId) {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null) {
            return ServiceResult<bool>.Error(ServiceResultStatus.NotFound, "User not found!");
        }

        var post = await _postRepository.GetPostByIdAsync(postId);
        if (post == null) {
            return ServiceResult<bool>.Error(ServiceResultStatus.NotFound, "Post not found!");
        }

        if (user.Id != post.UserId) {
            return ServiceResult<bool>.Error(ServiceResultStatus.Unauthorized, "User is not the owner of the post!");
        }

        await _postRepository.DeleteAsync(postId);

        return ServiceResult<bool>.Success(true);
    }

    public async Task<ServiceResult<PostCompleteDto>> PostVote(int userId, int postId, int value) {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null) {
            return ServiceResult<PostCompleteDto>.Error(ServiceResultStatus.NotFound, "User not found!");
        }

        var post = await _postRepository.GetPostByIdAsync(postId);
        if (post == null) {
            return ServiceResult<PostCompleteDto>.Error(ServiceResultStatus.NotFound, "Post not found!");
        }

        var existingVotePost = await _votePostRepository.GetVotePostByIdAsync(userId, postId);
        if (existingVotePost == null) {
            var newVotePost = new VotePost {
                UserId = userId,
                PostId = postId,
                Value = value
            };
            await _votePostRepository.AddVotePostAsync(newVotePost);
        }
        else if (existingVotePost.Value != value) {
            existingVotePost.Value = value;
            await _votePostRepository.UpdateVotePostAsync(existingVotePost);
        }
        else {
            await _votePostRepository.DeleteVotePostAsync(userId, postId);
        }

        return ServiceResult<PostCompleteDto>.Success(new PostCompleteDto {
            Id = post.Id,
            Title = post.Title,
            Text = post.Text,
            User = new UserSimpleDto { Id = post.User.Id, Username = post.User.Username },
            Created = post.CreatedAt,
            Updated = post.UpdatedAt,
            Votes = post.Votes.Aggregate(0, (acc, curr) => acc + curr.Value),
            Community = new CommunitySimpleDto {
                Id = post.Community.Id,
                Name = post.Community.Name,
                Description = post.Community.Description,
                MembersCount = post.Community.Members.Count,
                UserId = post.Community.UserId,
                isMember = post.Community.Members.Any(m => m.UserId == userId),
                isOwner = post.Community.UserId == userId
            },
            Comments = post.Comments.Select(c => new CommentDto {
                Id = c.Id,
                Author = new UserSimpleDto { Id = c.User.Id, Username = c.User.Username },
                CreatedAt = c.CreatedAt,
                Text = c.Text,
                UpdatedAt = c.UpdatedAt,
                Votes = c.Votes.Aggregate(0, (acc, curr) => acc + curr.Value)
            }).ToList(),
            CommentsCount = post.Comments.Count
        });
    }

    public async Task<ServiceResult<PostCompleteDto>> UpdatePost(int userId, int postId, UpdatePostDto dto) {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null) {
            return ServiceResult<PostCompleteDto>.Error(ServiceResultStatus.NotFound, "User not found!");
        }

        var post = await _postRepository.GetPostByIdAsync(postId);
        if (post == null) {
            return ServiceResult<PostCompleteDto>.Error(ServiceResultStatus.NotFound, "Post not found!");
        }

        if (user.Id != post.UserId) {
            return ServiceResult<PostCompleteDto>.Error(ServiceResultStatus.Unauthorized, "User is not the owner of the post!");
        }

        post.Title = dto.Title;
        post.Text = dto.Text;
        post.UpdatedAt = DateTime.Now;

        await _postRepository.UpdateAsync(post);
        return ServiceResult<PostCompleteDto>.Success(new PostCompleteDto {
            Id = post.Id,
            Title = post.Title,
            Text = post.Text,
            User = new UserSimpleDto { Id = post.User.Id, Username = post.User.Username },
            Created = post.CreatedAt,
            Updated = post.UpdatedAt,
            Votes = post.Votes.Aggregate(0, (acc, curr) => acc + curr.Value),
            Community = new CommunitySimpleDto {
                Id = post.Community.Id,
                Name = post.Community.Name,
                Description = post.Community.Description,
                MembersCount = post.Community.Members.Count,
                UserId = post.Community.UserId,
                isMember = post.Community.Members.Any(m => m.UserId == userId),
                isOwner = post.Community.UserId == userId
            },
            Comments = post.Comments.Select(c => new CommentDto {
                Id = c.Id,
                Author = new UserSimpleDto { Id = c.User.Id, Username = c.User.Username },
                CreatedAt = c.CreatedAt,
                Text = c.Text,
                UpdatedAt = c.UpdatedAt,
                Votes = c.Votes.Aggregate(0, (acc, curr) => acc + curr.Value)
            }).ToList(),
            CommentsCount = post.Comments.Count
        });
    }
}