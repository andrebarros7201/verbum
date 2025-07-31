using Verbum.API.DTOs.User;
using Verbum.API.Results;

namespace Verbum.API.Interfaces.Services;

public interface IUserService {
    Task<ServiceResult<UserSimpleDto>> GetUser(string username);
    Task<ServiceResult<UserSimpleDto>> GetUserById(int userId);
    Task<ServiceResult<UserCompleteDto>> GetUserCompleteById(int id);
    Task<ServiceResult<UserSimpleDto>> UpdateUser(UpdateUserDto dto);
    Task<ServiceResult<bool>> DeleteUser(int id);
}