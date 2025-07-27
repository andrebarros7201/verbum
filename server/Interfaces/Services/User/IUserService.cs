using Verbum.API.DTOs.User;

namespace Verbum.API.Interfaces.Services;

public interface IUserService {
    Task<UserSimpleDto?> GetUser(string username);
    Task<UserCompleteDto?> GetUserCompleteById(int id);
    Task<UserSimpleDto> UpdateUser(UpdateUserDto dto);
    Task<bool> DeleteUser(int id);
}