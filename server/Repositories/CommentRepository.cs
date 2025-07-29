using Verbum.API.Data;
using Verbum.API.Interfaces.Repositories;
using Verbum.API.Models;

namespace Verbum.API.Repositories;

public class CommentRepository : ICommentRepository {
    private readonly AppDbContext _db;

    public CommentRepository(AppDbContext db) {
        _db = db;
    }

    public Task<Comment?> GetCommentByIdAsync(int id) {
        throw new NotImplementedException();
    }

    public async Task<Comment> AddAsync(Comment comment) {
        _db.Comments.Add(comment);
        await _db.SaveChangesAsync();
        return comment;
    }

    public Task<Comment> UpdateAsync(Comment comment) {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id) {
        throw new NotImplementedException();
    }
}