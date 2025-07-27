using Microsoft.EntityFrameworkCore;
using Verbum.API.Data;
using Verbum.API.Interfaces.Repositories;
using Verbum.API.Models;

namespace Verbum.API.Repositories;

public class PostRepository : IPostRepository {
    private readonly AppDbContext _db;

    public PostRepository(AppDbContext db) {
        _db = db;
    }

    public async Task<Post> GetPostByIdAsync(int id) {
        return await _db.Posts.Include(p => p.User).Include(p => p.Community).FirstOrDefaultAsync(p => p.Id == id);
    }

    public Task<List<Post>> GetPostsByCommunityIdAsync(int communityId) {
        throw new NotImplementedException();
    }

    public async Task<Post> AddAsync(Post post) {
        _db.Posts.Add(post);
        await _db.SaveChangesAsync();
        return post;
    }

    public Task<Post> UpdateAsync(Post post) {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(int id) {
        var post = await _db.Posts.FindAsync(id);
        if (post == null) {
            return false;
        }

        _db.Posts.Remove(post);
        await _db.SaveChangesAsync();
        return true;
    }
}