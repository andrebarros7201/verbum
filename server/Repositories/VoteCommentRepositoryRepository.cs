using Microsoft.EntityFrameworkCore.ChangeTracking;
using Verbum.API.Data;
using Verbum.API.Interfaces.Repositories;
using Verbum.API.Models;

namespace Verbum.API.Repositories;

public class VoteCommentRepositoryRepository : IVoteCommentRepository {
    private readonly AppDbContext _db;

    public VoteCommentRepositoryRepository(AppDbContext db) {
        _db = db;
    }

    public async Task<VoteComment?> GetVoteCommentByIdAsync(int UserId, int CommentId) {
        return await _db.VoteComments.FindAsync(UserId, CommentId);
    }

    public async Task<bool> DeleteVoteCommentAsync(int UserId, int CommentId) {
        var vc = await _db.VoteComments.FindAsync(UserId, CommentId);
        _db.VoteComments.Remove(vc);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<VoteComment> AddVoteCommentAsync(VoteComment voteComment) {
        EntityEntry<VoteComment> vc = await _db.VoteComments.AddAsync(voteComment);
        await _db.SaveChangesAsync();
        return (await GetVoteCommentByIdAsync(vc.Entity.UserId, vc.Entity.CommentId))!;
    }

    public async Task<VoteComment> UpdateVoteCommentAsync(VoteComment voteComment) {
        _db.VoteComments.Update(voteComment);
        await _db.SaveChangesAsync();
        return voteComment;
    }
}