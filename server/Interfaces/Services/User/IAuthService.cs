using Verbum.API.DTOs.User;

namespace Verbum.API.Interfaces.Services;

public interface IAuthService {
    Task<UserDto?> Login(UserLoginDto user);
    Task<bool> Register(CreateUserDto user);
}