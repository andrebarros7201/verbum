using Verbum.API.Models;

namespace Verbum.API.Interfaces.Repositories;

public interface IVoteCommentRepository {
    Task<VoteComment?> GetVoteCommentByIdAsync(int UserId, int CommentId);
    Task<VoteComment> AddVoteCommentAsync(VoteComment voteComment);
    Task<VoteComment> UpdateVoteCommentAsync(VoteComment voteComment);
    Task<bool> DeleteVoteCommentAsync(int UserId, int CommentId);
}