using Verbum.API.DTOs.User;
using Verbum.API.Results;

namespace Verbum.API.Interfaces.Services;

public interface IAuthService {
    Task<ServiceResult<UserSimpleDto>> Login(UserLoginDto user);
    Task<ServiceResult<UserSimpleDto>> Register(CreateUserDto user);
}