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
    private readonly IVoteCommentRepository _voteCommentRepository;

    public CommentService(ICommentRepository commentRepository, IUserRepository userRepository, IPostRepository postRepository,
        IVoteCommentRepository voteCommentRepository) {
        _commentRepository = commentRepository;
        _userRepository = userRepository;
        _postRepository = postRepository;
        _voteCommentRepository = voteCommentRepository;
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
        // Automatically like the comment created
        await CommentVote(userId, comment.Id, 1);

        return ServiceResult<CommentDto>.Success(new CommentDto {
            Id = comment.Id,
            Text = comment.Text,
            Author = new UserSimpleDto { Id = comment.User.Id, Username = comment.User.Username },
            PostId = postId,
            CreatedAt = comment.CreatedAt,
            UpdatedAt = comment.UpdatedAt,
            Votes = comment.Votes.Aggregate(0, (acc, curr) => acc + curr.Value)
        });
    }

    public async Task<ServiceResult<CommentDto>> UpdateComment(int userId, int commentId, UpdateCommentDto dto) {
        var comment = await _commentRepository.GetCommentByIdAsync(commentId);
        if (comment == null) {
            return ServiceResult<CommentDto>.Error(ServiceResultStatus.NotFound, "Comment not found!");
        }

        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null) {
            return ServiceResult<CommentDto>.Error(ServiceResultStatus.NotFound, "User not found!");
        }

        if (user.Id != comment.UserId) {
            return ServiceResult<CommentDto>.Error(ServiceResultStatus.Unauthorized, "User is not the owner of the comment!");
        }

        comment.Text = dto.Text;
        comment.UpdatedAt = DateTime.Now;

        await _commentRepository.UpdateAsync(comment);
        return ServiceResult<CommentDto>.Success(new CommentDto {
            Id = comment.Id,
            Text = comment.Text,
            Author = new UserSimpleDto { Id = comment.User.Id, Username = comment.User.Username },
            CreatedAt = comment.CreatedAt,
            UpdatedAt = comment.UpdatedAt,
            Votes = comment.Votes.Aggregate(0, (acc, curr) => acc + curr.Value)
        });
    }

    public async Task<ServiceResult<bool>> DeleteComment(int userId, int commentId) {
        var comment = await _commentRepository.GetCommentByIdAsync(commentId);
        if (comment == null) {
            return ServiceResult<bool>.Error(ServiceResultStatus.NotFound, "Comment not found!");
        }

        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null) {
            return ServiceResult<bool>.Error(ServiceResultStatus.NotFound, "User not found!");
        }

        if (user.Id != comment.UserId) {
            return ServiceResult<bool>.Error(ServiceResultStatus.Unauthorized, "User is not the owner of the comment!");
        }

        await _commentRepository.DeleteAsync(commentId);
        return ServiceResult<bool>.Success(true);
    }

    public async Task<ServiceResult<CommentDto>> CommentVote(int userId, int commentId, int value) {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null) {
            return ServiceResult<CommentDto>.Error(ServiceResultStatus.NotFound, "User not found!");
        }

        var comment = await _commentRepository.GetCommentByIdAsync(commentId);
        if (comment == null) {
            return ServiceResult<CommentDto>.Error(ServiceResultStatus.NotFound, "Comment not found!");
        }

        var voteComment = await _voteCommentRepository.GetVoteCommentByIdAsync(userId, commentId);
        if (voteComment == null) {
            var newVoteComment = new VoteComment {
                UserId = userId,
                CommentId = commentId,
                Value = value
            };
            await _voteCommentRepository.AddVoteCommentAsync(newVoteComment);
        }
        else if (voteComment.Value != value) {
            voteComment.Value = value;
            await _voteCommentRepository.UpdateVoteCommentAsync(voteComment);
        }
        else {
            await _voteCommentRepository.DeleteVoteCommentAsync(voteComment);
        }


        return ServiceResult<CommentDto>.Success(new CommentDto {
            Id = comment.Id,
            Text = comment.Text,
            Author = new UserSimpleDto { Id = comment.User.Id, Username = comment.User.Username },
            CreatedAt = comment.CreatedAt,
            UpdatedAt = comment.UpdatedAt,
            Votes = comment.Votes?.Aggregate(0, (acc, curr) => acc + curr.Value) ?? 0
        });
    }
}