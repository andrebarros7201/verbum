using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Verbum.API.Data;
using Verbum.API.Interfaces.Repositories;
using Verbum.API.Models;

namespace Verbum.API.Repositories;

public class UserRepository : IUserRepository {
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db) {
        _db = db;
    }

    public async Task<User> GetUserByIdAsync(int id) {
        return await _db.Users.Include(u => u.Posts)
            .Include(u => u.Comments).ThenInclude(c => c.Votes)
            .Include(u => u.CommunitiesJoined).ThenInclude(uc => uc.Community)
            .Include(u => u.VotePosts)
            .FirstOrDefaultAsync(u => u.Id == id);
    }


    /// <summary>
    ///     Finds the user on db
    /// </summary>
    /// <param name="username">Users Username</param>
    /// <returns>User record</returns>
    public async Task<User?> GetUserByUsernameAsync(string username) {
        return await _db.Users.Include(u => u.Posts)
            .Include(u => u.Comments)
            .Include(u => u.CommunitiesJoined).ThenInclude(uc => uc.Community)
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    /// <summary>
    ///     Create a new user
    /// </summary>
    /// <param name="user">User object</param>
    /// <returns>User record</returns>
    public async Task<User?> AddAsync(User? user) {
        EntityEntry<User?> newUser = _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return newUser.Entity;
    }

    public async Task<User> UpdateAsync(User user) {
        var existingUser = await _db.Users.FindAsync(user.Id);
        if (existingUser == null) {
            return null;
        }

        existingUser.Username = user.Username;
        existingUser.Password = user.Password;
        await _db.SaveChangesAsync();
        return existingUser;
    }

    public async Task<bool> DeleteAsync(int id) {
        var user = await _db.Users.FindAsync(id);

        // User not found
        if (user == null) {
            return false;
        }

        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
        return true;
    }
}