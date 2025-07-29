using Verbum.API.DTOs.Comment;
using Verbum.API.DTOs.User;
using Verbum.API.Interfaces.Repositories;
using Verbum.API.Interfaces.Services;
using Verbum.API.Models;
using Verbum.API.Results;

namespace Verbum.API.Services;

public class CommentService : ICommentService {
    private readonly ICommentRepository _commentRepository;
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;

    public CommentService(ICommentRepository commentRepository, IUserRepository userRepository, IPostRepository postRepository) {
        _commentRepository = commentRepository;
        _userRepository = userRepository;
        _postRepository = postRepository;
    }


    public async Task<ServiceResult<CommentDto>> AddComment(int userId, int postId, CreateCommentDto dto) {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null) {
            return ServiceResult<CommentDto>.Error(ServiceResultStatus.NotFound, "User not found!");
        }

        var post = await _postRepository.GetPostByIdAsync(postId);
        if (post == null) {
            return ServiceResult<CommentDto>.Error(ServiceResultStatus.NotFound, "Post not found!");
        }

        var newComment = new Comment {
            Text = dto.Text,
            UserId = userId,
            User = user,
            PostId = postId,
            Post = post
        };

        var comment = await _commentRepository.AddAsync(newComment);
        return ServiceResult<CommentDto>.Success(new CommentDto {
            Id = comment.Id,
            Text = comment.Text,
            Author = new UserSimpleDto { Id = comment.User.Id, Username = comment.User.Username },
            CreatedAt = comment.CreatedAt,
            UpdatedAt = comment.UpdatedAt,
            Votes = comment.Votes.Aggregate(0, (acc, curr) => acc + curr.Value)
        });
    }

    public Task<ServiceResult<CommentDto>> UpdateComment(int userId, int commentId, UpdateCommentDto dto) {
        throw new NotImplementedException();
    }

    public Task<ServiceResult<bool>> DeleteComment(int userId, int commentId) {
        throw new NotImplementedException();
    }

    public Task<ServiceResult<CommentDto>> PostVote(int userId, int commentId, int value) {
        throw new NotImplementedException();
    }
}