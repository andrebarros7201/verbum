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
        return await _db.Users.FindAsync(id);
    }

    public async Task<User> AddAsync(User user) {
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
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
        if (user == null) {
            return false;
        }

        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
        return true;
    }
}