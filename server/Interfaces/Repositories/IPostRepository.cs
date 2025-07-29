using Verbum.API.Models;

namespace Verbum.API.Interfaces.Repositories;

public interface IPostRepository {
    Task<Post?> GetPostByIdAsync(int id);
    Task<Post> AddAsync(Post post);
    Task<Post> UpdateAsync(Post post);
    Task<bool> DeleteAsync(int id);
}