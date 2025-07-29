using Verbum.API.Data;
using Verbum.API.Interfaces.Repositories;
using Verbum.API.Models;

namespace Verbum.API.Repositories;

public class VotePostRepository : IVotePostRepository {

    private readonly AppDbContext _db;

    public VotePostRepository(AppDbContext db) {
        _db = db;
    }

    public async Task<VotePost?> GetVotePostByIdAsync(int UserId, int PostId) {
        return await _db.VotePosts.FindAsync(UserId, PostId);
    }

    public async Task<VotePost> UpdateVotePostAsync(VotePost votePost) {
        _db.VotePosts.Update(votePost);
        await _db.SaveChangesAsync();
        return votePost;
    }

    public async Task<bool> DeleteVotePostAsync(int UserId, int PostId) {
        var votePost = await _db.VotePosts.FindAsync(UserId, PostId);
        if (votePost == null) {
            return false;
        }

        _db.VotePosts.Remove(votePost);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<VotePost> AddVotePostAsync(VotePost votePost) {
        _db.VotePosts.Add(votePost);
        await _db.SaveChangesAsync();
        return votePost;
    }
}