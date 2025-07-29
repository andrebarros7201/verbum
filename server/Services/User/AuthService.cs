using Verbum.API.DTOs.User;
using Verbum.API.Interfaces.Repositories;
using Verbum.API.Interfaces.Services;
using Verbum.API.Models;
using Verbum.API.Results;

namespace Verbum.API.Services;

public class AuthService : IAuthService {
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository) {
        _userRepository = userRepository;
    }

    public async Task<ServiceResult<bool>> Register(CreateUserDto user) {
        var existingUser = await _userRepository.GetUserByUsernameAsync(user.Username);

        // User exists
        if (existingUser != null) {
            return ServiceResult<bool>.Error(ServiceResultStatus.Conflict, "User already exists!");
        }

        var newUser = new User {
            Username = user.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(user.Password)
        };

        await _userRepository.AddAsync(newUser);
        return ServiceResult<bool>.Success(true);
    }

    public async Task<ServiceResult<UserSimpleDto>> Login(UserLoginDto user) {
        var existingUser = await _userRepository.GetUserByUsernameAsync(user.Username);
        if (existingUser == null) {
            return ServiceResult<UserSimpleDto>.Error(ServiceResultStatus.NotFound, "User not found!");
        }

        // Check if hashed password matches password stored in db
        bool isMatch = BCrypt.Net.BCrypt.Verify(user.Password, existingUser.Password);

        return isMatch
            ? ServiceResult<UserSimpleDto>.Success(
                new UserSimpleDto {
                    Id = existingUser.Id,
                    Username = existingUser.Username
                })
            : ServiceResult<UserSimpleDto>.Error(ServiceResultStatus.Unauthorized, "Invalid credentials!");
    }
}