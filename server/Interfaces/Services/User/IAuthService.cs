using Verbum.API.DTOs.User;

namespace Verbum.API.Interfaces.Services;

public interface IAuthService {
    Task<UserCompleteDto?> Login(UserLoginDto user);
    Task<bool> Register(CreateUserDto user);
}