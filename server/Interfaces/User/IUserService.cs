using Verbum.API.DTOs.User;

namespace Verbum.API.Interfaces.User;

public interface IUserService {
    Task<UserDto> GetUser(int id);
    Task<bool> AddUser(CreateUserDto user);
    Task<bool> UpdateUser(UpdateUserDto user);
    Task<bool> DeleteUser(int id);
}