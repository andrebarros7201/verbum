using Verbum.API.Models;

namespace Verbum.API.Interfaces.Repositories;

public interface IUserRepository {
    Task<User> GetUserByIdAsync(int id);
    Task<User> GetUserByUsernameAsync(string username);
    Task<User> AddAsync(User user);
    Task<User> UpdateAsync(User user);
    Task<bool> DeleteAsync(int id);
}