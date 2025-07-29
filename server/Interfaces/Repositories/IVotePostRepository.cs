using Verbum.API.Models;

namespace Verbum.API.Interfaces.Repositories;

public interface IVotePostRepository {
    Task<VotePost?> GetVotePostByIdAsync(int UserId, int PostId);
    Task<VotePost> AddVotePostAsync(VotePost votePost);
    Task<VotePost> UpdateVotePostAsync(VotePost votePost);
    Task<bool> DeleteVotePostAsync(int UserId, int PostId);
}