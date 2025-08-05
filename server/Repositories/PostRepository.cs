using Microsoft.EntityFrameworkCore;
using Verbum.API.Data;
using Verbum.API.Interfaces.Repositories;
using Verbum.API.Models;

namespace Verbum.API.Repositories;

public class PostRepository : IPostRepository {
    private readonly ICommentRepository _commentRepository;
    private readonly AppDbContext _db;
    private readonly IVoteCommentRepository _voteCommentRepository;
    private readonly IVotePostRepository _votePostRepository;

    public PostRepository(AppDbContext db) {
        _db = db;
    }

    public async Task<Post?> GetPostByIdAsync(int id) {
        return await _db.Posts
            .Include(p => p.User)
            .Include(p => p.Community)
            .ThenInclude(c => c.Members)
            .Include(p => p.Votes)
            .Include(p => p.Comments).ThenInclude(c => c.Votes)
            .Include(p => p.Comments).ThenInclude(c => c.User)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Post> AddAsync(Post post) {
        _db.Posts.Add(post);
        await _db.SaveChangesAsync();
        return post;
    }

    public async Task<Post> UpdateAsync(Post post) {
        _db.Posts.Update(post);
        await _db.SaveChangesAsync();
        return post;
    }

    public async Task<bool> DeleteAsync(int id) {
        var post = await _db.Posts.Include(p => p.Comments).ThenInclude(c => c.Votes)
            .Include(p => p.Votes)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (post == null) {
            return false;
        }

        foreach (var comment in post.Comments) {
            _db.VoteComments.RemoveRange(comment.Votes);
        }

        _db.Comments.RemoveRange(post.Comments);
        _db.VotePosts.RemoveRange(post.Votes);

        _db.Posts.Remove(post);
        await _db.SaveChangesAsync();
        return true;
    }
}