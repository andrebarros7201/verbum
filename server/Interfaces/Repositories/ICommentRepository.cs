using Verbum.API.Models;

namespace Verbum.API.Interfaces.Repositories;

public interface ICommentRepository {
    Task<Comment?> GetCommentByIdAsync(int id);
    Task<Comment> AddAsync(Comment comment);
    Task<Comment> UpdateAsync(Comment comment);
    Task<bool> DeleteAsync(int id);
}