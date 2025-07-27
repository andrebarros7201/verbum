using Verbum.API.DTOs.User;
using Verbum.API.Interfaces.Repositories;
using Verbum.API.Interfaces.Services;
using Verbum.API.Models;

namespace Verbum.API.Services;

public class AuthService : IAuthService {
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository) {
        _userRepository = userRepository;
    }

    public async Task<UserSimpleDto?> Login(UserLoginDto user) {
        var existingUser = await _userRepository.GetUserByUsernameAsync(user.Username);
        if (existingUser == null) {
            return null;
        }

        // Check if hashed password matches password stored in db
        bool isMatch = BCrypt.Net.BCrypt.Verify(user.Password, existingUser.Password);

        return isMatch ? new UserSimpleDto { Id = existingUser.Id, Username = existingUser.Username } : null;
    }

    public async Task<bool> Register(CreateUserDto user) {
        var existingUser = await _userRepository.GetUserByUsernameAsync(user.Username);

        // User exists
        if (existingUser != null) {
            return false;
        }

        var newUser = new User {
            Username = user.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(user.Password)
        };

        await _userRepository.AddAsync(newUser);
        return true;
    }
}