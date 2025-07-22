using Verbum.API.DTOs.User;
using Verbum.API.Interfaces.User;

namespace Verbum.API.Services;

public class UserService : IUserService {

    public Task<UserDto> GetUser(int id) {
        throw new NotImplementedException();
    }

    public Task<bool> AddUser(CreateUserDto user) {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateUser(UpdateUserDto user) {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteUser(int id) {
        throw new NotImplementedException();
    }
}