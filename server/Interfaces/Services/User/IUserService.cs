using Verbum.API.DTOs.User;

namespace Verbum.API.Interfaces.Services;

public interface IUserService {
    Task<UserDto> GetUser(string username);
    Task<UserDto> UpdateUser(UpdateUserDto dto);
    Task<bool> DeleteUser(int id);
}