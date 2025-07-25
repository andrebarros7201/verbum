using Verbum.API.DTOs.User;
using Verbum.API.Interfaces.Repositories;
using Verbum.API.Interfaces.Services;

namespace Verbum.API.Services;

public class UserService : IUserService {
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository) {
        _userRepository = userRepository;
    }

    public async Task<UserDto> GetUser(string username) {
        var user = await _userRepository.GetUserByUsernameAsync(username);

        return user == null ? null : new UserDto { Id = user.Id, Username = user.Username };
    }

    public async Task<UserDto> UpdateUser(UpdateUserDto dto) {
        var user = await _userRepository.GetUserByIdAsync(dto.Id);
        if (user == null) {
            return null;
        }

        user.Username = dto.Username;
        user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var updatedUser = await _userRepository.UpdateAsync(user);
        return updatedUser == null ? null : new UserDto { Id = updatedUser.Id, Username = updatedUser.Username };
    }

    public async Task<bool> DeleteUser(int id) {
        if (id == null) {
            return false;
        }

        bool result = await _userRepository.DeleteAsync(id);
        return result;
    }
}