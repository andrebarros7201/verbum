using Verbum.API.DTOs.User;

namespace Verbum.API.Interfaces.Services;

public interface IUserService {
    Task<UserDto> GetUser(string username);
    Task<UserDto> AddUser(CreateUserDto user);
    Task<UserDto> UpdateUser(UpdateUserDto user);
    Task<bool> DeleteUser(int id);
}