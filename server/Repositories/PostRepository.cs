using Verbum.API.Data;
using Verbum.API.Interfaces.Repositories;
using Verbum.API.Models;

namespace Verbum.API.Repositories;

public class PostRepository : IPostRepository {
    private readonly AppDbContext _db;

    public PostRepository(AppDbContext db) {
        _db = db;
    }

    public Task<Post> GetPostByIdAsync(int id) {
        throw new NotImplementedException();
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

    public Task<bool> DeleteAsync(int id) {
        throw new NotImplementedException();
    }
}